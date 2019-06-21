using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Models;
using DataAccess.ViewModels;
using DataAccess.Context;
using System.Data.Entity;

namespace Common.Repository.Application
{
    public class TaskRepository : ITaskRepository
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

        public List<Task> Get()
        {
            var get = applicationContext.Tasks
                .Include("Status")
                .Include("Project")
                .Include("AssignedByMember")
                .Include("AssignedByMember.Rule")
                .Include("AssignedToMember")
                .Include("AssignedToMember.Rule")
                .Where(x => x.IsDelete == false).ToList();
            return get;
        }

        public Task Get(int id)
        {
            var get = applicationContext.Tasks.Find(id);
            return get;
        }

        public List<Task> GetSearch(string values)
        {
            var get = applicationContext.Tasks
                .Include("Status")
                .Include("Project")
                .Include("AssignedByMember")
                .Include("AssignedByMember.Rule")
                .Include("AssignedToMember")
                .Include("AssignedToMember.Rule")
                .Where(x => (
                    x.Priority.ToString().Contains(values)||
                    x.Status.Id.ToString().Contains(values)||
                    x.Project.Project_name.Contains(values)||
                    x.AssignedByMember.Rule.Rule_Name.Contains(values)||
                    x.AssignedToMember.Rule.Rule_Name.Contains(values)
                    ) && x.IsDelete == false).ToList();
            return get;
        }

        public bool insert(TaskVM taskVM)
        {
            var push = new Task(taskVM);
            var getStatus = applicationContext.Statuses.Find(taskVM.Status_Id);
            var getProject = applicationContext.Projects.Find(taskVM.Project_Id);
            var getProjectByMember = applicationContext.ProjectMembers.Find(taskVM.Assigned_By_Member);
            var getProjectToMember = applicationContext.ProjectMembers.Find(taskVM.Assigned_To_Member);
            if (getProjectByMember != null && getProjectToMember != null&& getStatus != null && getProject != null)
            {
                push.AssignedByMember = getProjectByMember;
                push.AssignedToMember = getProjectToMember;
                push.Status = getStatus;
                push.Project = getProject;
                applicationContext.Tasks.Add(push);
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

        public bool update(int id, TaskVM taskVM)
        {
            var get = Get(id);
            if (get != null)
            {
                get.Update(id, taskVM);
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
