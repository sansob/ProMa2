using System.Collections.Generic;
using Common.Repository;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace BusinessLogic.Service.Application {
    public class ProjectService : IProjectService {
        private bool _status = false;
         
        public ProjectService() {
        }

        private readonly IProjectRepository _iProjectRepository;

        public ProjectService(IProjectRepository iProjectRepository) {
            _iProjectRepository = iProjectRepository;
        }

        public List<Project> Get() {
            return _iProjectRepository.Get();
        }

        public List<Project> GetSearch(string searchQuery) {
            return _iProjectRepository.GetSearch(searchQuery);
        }

        public Project Get(int id) {
            return _iProjectRepository.Get(id);
        }

        public bool Insert(ProjectVM projectVm) {
            if (string.IsNullOrEmpty(projectVm.Project_name)) {
                return _status;
            }
            else if (string.IsNullOrEmpty(projectVm.Project_Detail)) {
                return _status;
            }
            else if (string.IsNullOrEmpty(projectVm.Project_Deadline.ToString())) {
                return _status;
            }
            else if (string.IsNullOrEmpty(projectVm.Project_Start.ToString())) {
                return _status;
            }

            return _iProjectRepository.Insert(projectVm); 
        }

        public bool Update(int id, ProjectVM projectVm) {
            return _iProjectRepository.Update(id, projectVm);
        }

        public bool Delete(int id) {
            return _iProjectRepository.Delete(id);
        }

        public List<Project> GetProjectsByModule(string modulQuery)
        {
            return _iProjectRepository.GetProject(modulQuery);
        }
    }
}