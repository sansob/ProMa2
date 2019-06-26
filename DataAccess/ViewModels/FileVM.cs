using DataAccess.Models;

namespace DataAccess.ViewModels
{
    public class FileVM
    {
        public string File_name { get; set; }
        public string File_url { get; set; }
        public int File_uploaderId { get; set; }
        public int Project_Id { get; set; }

        public FileVM()
        {
            
        }
        

        public FileVM(string fileName, string fileUrl, int uploaderId, int projectId)
        {
            this.File_name = fileName;
            this.File_url = fileUrl;
            this.File_uploaderId = uploaderId;
            this.Project_Id = projectId;
        }

        public void Update(string fileName, string fileUrl, int uploaderId, int projectId) {
            this.File_name = fileName;
            this.File_url = fileUrl;
            this.File_uploaderId = uploaderId;
            this.Project_Id = projectId;
        }
    }
}