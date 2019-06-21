using System;
using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.ViewModels;
using Common.Repository;

namespace BusinessLogic.Service.Application
{
    public class ProjectMemberService : IProjectMemberService
    {
        public ProjectMemberService() { }
        bool status = false;
        private readonly IProjectMemberRepository iProjectMemberRepository;
        public ProjectMemberService(IProjectMemberRepository _iProjectMemberRepository)
        {
            iProjectMemberRepository = _iProjectMemberRepository;
        }

        public List<ProjectMember> Get()
        {
            return iProjectMemberRepository.Get();
        }

        public List<ProjectMember> GetSearch(string values)
        {
            return iProjectMemberRepository.GetSearch(values);
        }

        public ProjectMember Get(int id)
        {
            return iProjectMemberRepository.Get(id);
        }

        public bool insert(ProjectMemberVM projectMemberVM)
        {
            return iProjectMemberRepository.insert(projectMemberVM);
        }

        public bool update(int id, ProjectMemberVM projectMemberVM)
        {
            if (string.IsNullOrWhiteSpace(projectMemberVM.Project_Id.ToString()) && string.IsNullOrWhiteSpace(projectMemberVM.Rule_Id.ToString()) && string.IsNullOrWhiteSpace(projectMemberVM.User_Id.ToString()))
            {
                return status;
            }
            else
            {
                return iProjectMemberRepository.update(id, projectMemberVM);
            }
        }

        public bool delete(int id)
        {
            return iProjectMemberRepository.delete(id);
        }
    }
}
