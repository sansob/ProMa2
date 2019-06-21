using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace Common.Repository {
    public interface IStatusRepository {
        List<Status> Get();
//        List<Project> GetSearch(string searchQuery);
//        Project Get(int id);
//        bool Insert(StatusVM statusVm);
//        bool Update(int id, StatusVM statusVm);
//        bool Delete(int id);
    }
}