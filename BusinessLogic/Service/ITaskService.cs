using DataAccess.Models;
using DataAccess.ViewModels;
using System.Collections.Generic;

namespace BusinessLogic.Service
{
    public interface ITaskService
    {
        List<Task> Get();
        List<Task> GetSearch(string values);
        Task Get(int id);
        bool insert(TaskVM taskVM);
        bool update(int id, TaskVM taskVM);
        bool delete(int id);
    }
}
