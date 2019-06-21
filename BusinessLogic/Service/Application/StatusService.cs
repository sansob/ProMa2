using System.Collections.Generic;
using Common.Repository;
using DataAccess.Models;

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
    }
}