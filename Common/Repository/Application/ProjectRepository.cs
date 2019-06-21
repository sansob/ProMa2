using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Context;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace Common.Repository.Application {
    public class ProjectRepository : IProjectRepository {
        private readonly ApplicationContext _applicationContext = new ApplicationContext();
        private bool _status = false;

        public List<Project> Get() {
            return _applicationContext.Projects.Include("Status").Where(X => X.IsDelete == false).ToList();
        }

        public List<Project> GetSearch(string searchQuery) {
            return _applicationContext.Projects
                .Where(x => x.IsDelete == false &&
                            (x.Id.ToString().Contains(searchQuery) || x.Project_name.Contains(searchQuery)))
                .ToList();
        }

        public Project Get(int id) {
            return _applicationContext.Projects.Find(id);
        }

        public bool Insert(ProjectVM projectVm) {
            var push = new Project(projectVm);
            var getStatus = _applicationContext.Statuses.Find(projectVm.Status_Id);
            if (getStatus != null) {
                push.Status = getStatus;
                _applicationContext.Projects.Add(push);
                var result = _applicationContext.SaveChanges();
                if (result > 0) {
                    _status = true;
                }
                else {
                    return _status;
                }
            }

            return _status;
        }

        public bool Update(int id, ProjectVM projectVm) {
            var pull = Get(id);
            var getStatus = _applicationContext.Statuses.Find(projectVm.Status_Id);
            pull.Status = getStatus;
            pull.Update(projectVm);
            _applicationContext.Entry(pull).State = EntityState.Modified;
            var result = _applicationContext.SaveChanges();
            if (result > 0) {
                _status = true;
            }
            else {
                return _status;
            }

            return _status;
        }

        public bool Delete(int id) {
            var get = Get(id);
            if (get == null) {
                return false;
            }

            get.Delete();
            _applicationContext.Entry(get).State = System.Data.Entity.EntityState.Modified;
            _applicationContext.SaveChanges();
            return true;
        }
    }
}