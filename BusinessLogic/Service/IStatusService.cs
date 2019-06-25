using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace BusinessLogic.Service {
    public interface IStatusService {
        List<Status> Get();
        List<Status> GetStatusByModule(string modulQuery);
        Status Get(int id);
        bool Insert(StatusVM statusVm);
        bool Update(int id, StatusVM statusVm);
        bool Delete(int id);
    }
}