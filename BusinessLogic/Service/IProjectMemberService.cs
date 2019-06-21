using DataAccess.Models;
using DataAccess.ViewModels;
using System.Collections.Generic;

namespace BusinessLogic.Service
{
    public interface IProjectMemberService
    {
        List<ProjectMember> Get();
        List<ProjectMember> GetSearch(string values);
        ProjectMember Get(int id);
        bool insert(ProjectMemberVM projectMemberVM);
        bool update(int id, ProjectMemberVM projectMemberVM);
        bool delete(int id);
    }
}
