using System.Collections.Generic;
using Common.Repository;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace BusinessLogic.Service.Application {
    public class StatusService: IStatusService {

        public StatusService() {

        }

        private readonly IStatusRepository _iStatusRepository;

        public StatusService(IStatusRepository iStatusRepository) {
            _iStatusRepository = iStatusRepository;
        }

        public List<Status> Get() {
            return _iStatusRepository.Get();
        }

        public List<Status> GetStatusByModule(string modulQuery)
        {
            return _iStatusRepository.GetStatusByModule(modulQuery);
        }


        public Status Get(int id)
        {
            return _iStatusRepository.Get(id);
        }

        public bool Insert(StatusVM statusVm)
        {
            return _iStatusRepository.Insert(statusVm);
        }

        public bool Update(int id, StatusVM statusVm)
        {
            return _iStatusRepository.Update(id, statusVm);
        }

        public bool Delete(int id)
        {
            return _iStatusRepository.Delete(id);
        }
    }
}