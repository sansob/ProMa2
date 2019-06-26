﻿using DataAccess.Models;
using DataAccess.ViewModels;
using System.Collections.Generic;

namespace BusinessLogic.Service
{
    public interface IProjectMemberService
    {
        List<ProjectMember> Get();
        List<ProjectMember> GetSearch(string values);
        List<ProjectMember> GetProjectMemberByProjectId(int project_id);
        ProjectMember Get(int id);
        bool Insert(ProjectMemberVM projectMemberVM);
        bool Update(int id, ProjectMemberVM projectMemberVM);
        bool Delete(int id);
    }
}
