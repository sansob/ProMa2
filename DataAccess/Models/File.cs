using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base;
using DataAccess.ViewModels;

namespace DataAccess.Models
{
    [Table("TB_M_Files")]
    public class File : BaseModel
    {
        public string File_name { get; set; }
        public string File_url { get; set; }
        public int? File_uploaderId { get; set; }
        [ForeignKey("Project")] public int Project_Id { get; set; }
        public Project Project { get; set; }

        public File()
        {
        }

        public File(FileVM fileVm)
        {
            File_name = fileVm.File_name;
            File_url = fileVm.File_url;
            File_uploaderId = fileVm.File_uploaderId;
            CreateDate = DateTimeOffset.Now.LocalDateTime;
        }

        public void Update(FileVM fileVm)
        {
            File_name = fileVm.File_name;
            File_url = fileVm.File_url;
            File_uploaderId = fileVm.File_uploaderId;
            UpdateDate = DateTimeOffset.Now.LocalDateTime;
        }

        public void Delete()
        {
            IsDelete = true;
            DeleteDate = DateTimeOffset.Now.LocalDateTime;
        }
    }
}