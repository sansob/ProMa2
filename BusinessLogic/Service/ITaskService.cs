using DataAccess.Models;
using DataAccess.ViewModels;
using System.Collections.Generic;

namespace BusinessLogic.Service
{
    public interface ITaskService
    {
        List<Task> Get();
        List<Task> GetSearch(string values);
        List<Task> GetProjectName(string moduleQuery);
        List<Task> GetTaskByProjectId(int project_id);
        Task Get(int id);
        bool Insert(TaskVM taskVM);
        bool Update(int id, TaskVM taskVM);
        bool Delete(int id);
    }
}
