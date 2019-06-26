using System;
using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.ViewModels;
using Common.Repository;

namespace BusinessLogic.Service.Application
{
    public class TaskService : ITaskService
    {
        public TaskService() { }
        bool status = false;
        ITaskRepository iTaskRepository;
        public TaskService(ITaskRepository _iTaskRepository)
        {
            iTaskRepository = _iTaskRepository;
        }

        public List<Task> Get()
        {
            return iTaskRepository.Get();
        }

        public List<Task> GetSearch(string values)
        {
            return iTaskRepository.GetSearch(values);
        }

        public Task Get(int id)
        {
            return iTaskRepository.Get(id);
        }

        public List<Task> GetTaskByProjectId(int project_id)
        {
            return iTaskRepository.GetTaskByProjectId(project_id);
        }

        public bool Insert(TaskVM taskVM)
        {
            if (string.IsNullOrWhiteSpace(taskVM.Project_Id.ToString())&&
                string.IsNullOrWhiteSpace(taskVM.Status_Id.ToString()))
            {
                return status;
            }
            else
            {
                return iTaskRepository.Insert(taskVM);
            }
        }

        public bool Update(int id, TaskVM taskVM)
        {
            if (string.IsNullOrWhiteSpace(taskVM.Id.ToString()))
            {
                return status = false;
            }
            else
            {
                return iTaskRepository.Update(id, taskVM);
            }
        }

        public bool Delete(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return status = false;
            }
            else
            {
                return iTaskRepository.Delete(id);
            }
        }
    }
}
