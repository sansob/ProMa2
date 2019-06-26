using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Context;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace Common.Repository.Application
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationContext _applicationContext = new ApplicationContext();
        private bool _status;


        public List<File> Get()
        {
            return _applicationContext.Files.Include("Project").Where(X => X.IsDelete == false)
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<File> GetSearch(string searchQuery)
        {
            return _applicationContext.Files
                .Where(x => x.IsDelete == false &&
                            (x.Id.ToString().Contains(searchQuery) || x.File_name.Contains(searchQuery)))
                .ToList();
        }

        public List<File> GetFileByProject(int id)
        {
            return _applicationContext.Files.Include("Project").Where(X => X.IsDelete == false && X.Project_Id == id)
                .OrderByDescending(c => c.Id).ToList();
        }

        public File Get(int id)
        {
            return _applicationContext.Files.Include("Project").SingleOrDefault(x => x.Id == id && x.IsDelete == false);
        }

        public bool Insert(FileVM fileVM)
        {
            var push = new File(fileVM);
            var getProject = _applicationContext.Projects.Find(fileVM.Project_Id);
            if (getProject != null)
            {
                push.Project = getProject;
                _applicationContext.Files.Add(push);
                var result = _applicationContext.SaveChanges();
                if (result > 0)
                    _status = true;
                else
                    return _status;
            }

            return _status;
        }

        public bool Update(int id, FileVM fileVm)
        {
            var pull = Get(id);
            var getProject = _applicationContext.Projects.Find(fileVm.Project_Id);
            pull.Project = getProject;
            pull.Update(fileVm);
            _applicationContext.Entry(pull).State = EntityState.Modified;
            var result = _applicationContext.SaveChanges();
            if (result > 0)
                _status = true;
            else
                return _status;

            return _status;
        }

        public bool Delete(int id)
        {
            var get = Get(id);
            if (get == null) return false;

            get.Delete();
            _applicationContext.Entry(get).State = EntityState.Modified;
            _applicationContext.SaveChanges();
            return true;
        }
    }
}