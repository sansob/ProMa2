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
        public bool Delete(int id)
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
                .Include("Project")
                .Include("Status")
                .Where(x => x.IsDelete == false).ToList();
            return get;
        }

        public Task Get(int id)
        {
            var get = applicationContext.Tasks.Include("Project").Include("Status").SingleOrDefault(x=>x.Id==id);
            return get;
        }

        public List<Task> GetProjectName(string moduleQuery)
        {
            return applicationContext.Tasks.Include("Project").Include("Status").Where(x => x.IsDelete == false && x.Project.Project_name.Contains(moduleQuery)).ToList();
        }

        public List<Task> GetSearch(string values)
        {
            var get = applicationContext.Tasks
                .Include("Project")
                .Include("Status")
                .Where(x => (
                    x.Priority.ToString().Contains(values)||
                    x.Status_Id.ToString().Contains(values)||
                    x.Project.Project_name.Contains(values)||
                    x.Status.Status_name.Contains(values)
                    ) && x.IsDelete == false).ToList();
            return get;
        }

        public List<Task> GetTaskByProjectId(int project_id)
        {
            var get = applicationContext.Tasks
                .Include("Status")
                .Include("Project")
                .Where(x => x.Project_Id.Equals(project_id) && x.IsDelete == false).ToList();
            return get;
        }

        public bool Insert(TaskVM taskVM)
        {
            var push = new Task(taskVM);
            var getStatus = applicationContext.Statuses.Find(taskVM.Status_Id);
            var getProject = applicationContext.Projects.Find(taskVM.Project_Id);
            if (getStatus != null && getProject != null)
            {
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

        public bool Update(int id, TaskVM taskVM)
        {
            var pull = Get(id);
            var getStatus = applicationContext.Statuses.Find(taskVM.Status_Id);
            var getProject = applicationContext.Projects.Find(taskVM.Project_Id);
            if(getStatus!=null && getProject != null)
            {
                pull.Status = getStatus;
                pull.Project = getProject;
                pull.Update(taskVM);
                applicationContext.Entry(pull).State = EntityState.Modified;
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
    }
}
