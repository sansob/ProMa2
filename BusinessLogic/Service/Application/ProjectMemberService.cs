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

        public List<ProjectMember> GetProjectMemberByProjectId(int project_id)
        {
            return iProjectMemberRepository.GetProjectMemberByProjectId(project_id);
        }

        public bool Insert(ProjectMemberVM projectMemberVM)
        {
            if(string.IsNullOrWhiteSpace(projectMemberVM.Project_Id.ToString())&&
                string.IsNullOrWhiteSpace(projectMemberVM.Rule_Id.ToString())&&
                string.IsNullOrWhiteSpace(projectMemberVM.User_Id.ToString()))
            {
                return status;
            }
            else
            {
                return iProjectMemberRepository.Insert(projectMemberVM);
            }
        }

        public bool Update(int id, ProjectMemberVM projectMemberVM)
        {
            if (string.IsNullOrWhiteSpace(projectMemberVM.Project_Id.ToString()))
            {
                return status;
            }
            else
            {
                return iProjectMemberRepository.Update(id, projectMemberVM);
            }
        }
        public bool Delete(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return status;
            }
            else
            {
                return iProjectMemberRepository.Delete(id);
            }
        }
    }
}
