using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.ViewModels;
using DataAccess.Context;
using System.Data.Entity;

namespace Common.Repository.Application
{
    public class ProjectMemberRepository : IProjectMemberRepository
    {
        ApplicationContext applicationContext = new ApplicationContext();
        bool status = false;
        public bool delete(int id)
        {
            var get = Get(id);
            if (get != null)
            {
                get.Delete();
                applicationContext.Entry(get).State = EntityState.Modified;
                var result = applicationContext.SaveChanges();
                return result > 0;
            }
            else
            {
                return false;
            }
        }

        public List<ProjectMember> Get()
        {
            var get = applicationContext.ProjectMembers.Include("Project").Include("Rule").Where(x => x.IsDelete == false).ToList();
            return get;
        }

        public ProjectMember Get(int id)
        {
            var get = applicationContext.ProjectMembers.Find(id);
            return get;
        }

        public List<ProjectMember> GetSearch(string values)
        {
            var get = applicationContext.ProjectMembers.Include("Project").Include("Rule").Where(x => (x.Project.Project_name.Contains(values) || x.Rule.Rule_Name.Contains(values) || x.Id.ToString().Contains(values)) && x.IsDelete == false).ToList();
            return get;
        }

        public bool insert(ProjectMemberVM projectMemberVM)
        {
            var push = new ProjectMember(projectMemberVM);
            var getRule = applicationContext.Rules.Find(projectMemberVM.Rule_Id);
            var getProject = applicationContext.Projects.Find(projectMemberVM.Project_Id);
            if(getRule!=null && getProject != null)
            {
                push.Rule = getRule;
                push.Project = getProject;
                applicationContext.ProjectMembers.Add(push);
                var result = applicationContext.SaveChanges();
                if (result > 0)
                {
                    status = true;
                    return status;
                }
                else
                {
                    return status;
                }
            }
            else
            {
                return status;
            }
        }

        public bool update(int id, ProjectMemberVM projectMemberVM)
        {
            var get = Get(id);
            if(get != null)
            {
                get.Update(id, projectMemberVM);
                applicationContext.Entry(get).State = EntityState.Modified;
                applicationContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
