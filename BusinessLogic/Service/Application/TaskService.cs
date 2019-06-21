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

        public bool insert(TaskVM taskVM)
        {
            if (string.IsNullOrWhiteSpace(taskVM.Project_Id.ToString())&&string.IsNullOrWhiteSpace(taskVM.Status_Id.ToString())&&string.IsNullOrWhiteSpace(taskVM.Assigned_By_Member.ToString())&&string.IsNullOrWhiteSpace(taskVM.Assigned_To_Member.ToString()))
            {
                return status;
            }
            else
            {
                return iTaskRepository.insert(taskVM);
            }
        }

        public bool update(int id, TaskVM taskVM)
        {
            return iTaskRepository.update(id, taskVM);
        }

        public bool delete(int id)
        {
            return iTaskRepository.delete(id);
        }
    }
}
