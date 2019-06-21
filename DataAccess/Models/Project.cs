using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using Core.Base;
using DataAccess.ViewModels;

namespace DataAccess.Models {
    [Table("TB_M_Projects")]
    public class Project : BaseModel {
        public string Project_name { get; set; }
        public DateTimeOffset Project_Start { get; set; }
        public DateTimeOffset Project_Deadline { get; set; }
        public string Project_Detail { get; set; }
        [ForeignKey("Status")] public int Status_Id { get; set; }
        public Status Status { get; set; }

        public Project() {
        }

        public Project(ProjectVM projectVm) {
            this.Project_name = projectVm.Project_name;
            this.Project_Deadline = projectVm.Project_Deadline;
            this.Project_Start = projectVm.Project_Start;
            this.Project_Detail = projectVm.Project_Detail;
            this.CreateDate = DateTimeOffset.Now.LocalDateTime;
        }

        public void Update(ProjectVM projectVm) {
            this.Project_name = projectVm.Project_name;
            this.Project_Deadline = projectVm.Project_Deadline;
            this.Project_Start = projectVm.Project_Start;
            this.Project_Detail = projectVm.Project_Detail;
            this.UpdateDate = DateTimeOffset.Now.LocalDateTime;
        }

        public void Delete() {
            this.IsDelete = true;
            this.DeleteDate = DateTimeOffset.Now.LocalDateTime;
        }
    }
}