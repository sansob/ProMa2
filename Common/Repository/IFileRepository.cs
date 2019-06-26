using System.Collections.Generic;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace Common.Repository
{
    public interface IFileRepository
    {
        List<File> Get();
        List<File> GetSearch(string searchQuery);
        File Get(int id);
        bool Insert(FileVM fileVM);
        bool Update(int id, FileVM fileVm);
        bool Delete(int id);
    }
}