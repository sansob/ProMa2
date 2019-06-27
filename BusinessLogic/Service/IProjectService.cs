using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace BusinessLogic.Service {
    public interface IProjectService {
        List<Project> GetProjectsByModule(string modulQuery);
        List<Project> Get();
        List<Project> GetSearch(string searchQuery);
        Project Get(int id);
        bool Insert(ProjectVM projectVm);
        bool Update(int id, ProjectVM projectVm);
        bool Delete(int id);
    }
}