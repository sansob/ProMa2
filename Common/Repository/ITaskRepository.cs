using DataAccess.Models;
using DataAccess.ViewModels;
using System.Collections.Generic;

namespace Common.Repository
{
    public interface ITaskRepository
    {
        List<Task> Get();
        List<Task> GetSearch(string values);
        Task Get(int id);
        bool insert(TaskVM taskVM);
        bool update(int id, TaskVM taskVM);
        bool delete(int id);
    }
}
