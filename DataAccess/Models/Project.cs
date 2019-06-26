using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base;
using DataAccess.ViewModels;

namespace DataAccess.Models {
    [Table("TB_M_Projects")]
    public class Project : BaseModel {
        public string Project_name { get; set; }
        public DateTimeOffset Project_Start { get; set; }
        public DateTimeOffset Project_Deadline { get; set; }
        public string Project_Detail { get; set; }
        public int? Project_OwnerId { get; set; }
        [ForeignKey("Status")] public int Status_Id { get; set; }
        public Status Status { get; set; }

        public Project() {
        }

        public Project(ProjectVM projectVm) {
            Project_name = projectVm.Project_name;
            Project_Deadline = projectVm.Project_Deadline;
            Project_Start = projectVm.Project_Start;
            Project_Detail = projectVm.Project_Detail;
            Project_OwnerId = projectVm.Project_OwnerId;
            CreateDate = DateTimeOffset.Now.LocalDateTime;
        }

        public void Update(ProjectVM projectVm) {
            Project_name = projectVm.Project_name;
            Project_Deadline = projectVm.Project_Deadline;
            Project_Start = projectVm.Project_Start;
            Project_Detail = projectVm.Project_Detail;
            Project_OwnerId = projectVm.Project_OwnerId;
            UpdateDate = DateTimeOffset.Now.LocalDateTime;
        }

        public void Delete() {
            IsDelete = true;
            DeleteDate = DateTimeOffset.Now.LocalDateTime;
        }
    }
}