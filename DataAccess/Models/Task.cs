using Core.Base;
using DataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [Table("TB_T_Tasks")]
    public class Task : BaseModel
    {
        [ForeignKey("Project")]
        public int Project_Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Start_Date { get; set; }
        public DateTimeOffset Due_Date { get; set; }
        public int Assigned_By_Member { get; set; }
        public int Priority { get; set; }
        [ForeignKey("Status")]
        public int Status_Id { get; set; }
        public int Assigned_To_Member { get; set; }
        
        public Project Project { get; set; }
        public Status Status { get; set; }

        public Task() { }
        public Task(TaskVM taskVM)
        {
            this.Description = taskVM.Description;
            this.Start_Date = taskVM.Start_Date;
            this.Due_Date = taskVM.Due_Date;
            this.Priority = taskVM.Priority;
            this.CreateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public void Update(TaskVM taskVM)
        {
            this.Id = taskVM.Id;
            this.Description = taskVM.Description;
            this.Start_Date = taskVM.Start_Date;
            this.Due_Date = taskVM.Due_Date;
            this.Priority = taskVM.Priority;
            this.UpdateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public void Delete()
        {
            this.IsDelete = true;
            this.DeleteDate = DateTimeOffset.Now.ToLocalTime();
        }
    }
}
