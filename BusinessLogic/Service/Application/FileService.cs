using System.Collections.Generic;
using Common.Repository;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace BusinessLogic.Service.Application
{
    public class FileService : IFileService
    {

        public FileService()
        {
        }

        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository iFileRepository)
        {
            _fileRepository = iFileRepository;
        }

        public List<File> Get()
        {
            return _fileRepository.Get();
        }

        public List<File> GetSearch(string searchQuery)
        {
            return _fileRepository.GetSearch(searchQuery);
        }

        public File Get(int id)
        {
            return _fileRepository.Get(id);
        }

        public bool Insert(FileVM fileVm)
        {
            return _fileRepository.Insert(fileVm);
        }

        public bool Update(int id, FileVM fileVm)
        {
            return _fileRepository.Update(id, fileVm);
        }

        public bool Delete(int id)
        {
            return _fileRepository.Delete(id);
        }
    }
}