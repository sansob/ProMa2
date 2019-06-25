using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Context;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace Common.Repository.Application
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationContext _applicationContext = new ApplicationContext();
        private bool _status;

        public List<Status> Get()
        {
            return _applicationContext.Statuses.Where(X => X.IsDelete == false).OrderByDescending(c => c.Id).ToList();
        }

        public List<Status> GetStatusByModule(string modulQuery)
        {
            return _applicationContext.Statuses.Where(X => X.IsDelete == false && X.Status_module.Contains(modulQuery))
                .ToList();
        }


        public Status Get(int id)
        {
            return _applicationContext.Statuses.Find(id);
        }

        public bool Insert(StatusVM statusVm)
        {
            var push = new Status(statusVm);

            _applicationContext.Statuses.Add(push);
            var result = _applicationContext.SaveChanges();
            if (result > 0)
                _status = true;
            else
                return _status;

            return _status;
        }

        public bool Update(int id, StatusVM statusVm)
        {
            var pull = Get(id);
            pull.Update(statusVm);
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