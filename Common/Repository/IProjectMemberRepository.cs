using DataAccess.Models;
using DataAccess.ViewModels;
using System.Collections.Generic;

namespace Common.Repository
{
    public interface IProjectMemberRepository
    {
        List<ProjectMember> Get();
        List<ProjectMember> GetSearch(string values);
        ProjectMember Get(int id);
        bool insert(ProjectMemberVM projectMemberVM);
        bool update(int id, ProjectMemberVM projectMemberVM);
        bool delete(int id);
    }
}
