using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace BusinessLogic.Service
{
    public interface IFileService
    {
        List<File> Get();
        List<File> GetSearch(string searchQuery);
        File Get(int id);
        bool Insert(FileVM fileVm);
        bool Update(int id, FileVM fileVm);
        bool Delete(int id);
    }
}