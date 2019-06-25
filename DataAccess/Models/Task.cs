using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base;
using DataAccess.ViewModels;

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
            Description = taskVM.Description;
            Start_Date = taskVM.Start_Date;
            Due_Date = taskVM.Due_Date;
            Priority = taskVM.Priority;
            CreateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public void Update(TaskVM taskVM)
        {
            Id = taskVM.Id;
            Description = taskVM.Description;
            Start_Date = taskVM.Start_Date;
            Due_Date = taskVM.Due_Date;
            Priority = taskVM.Priority;
            UpdateDate = DateTimeOffset.Now.ToLocalTime();
        }
        public void Delete()
        {
            IsDelete = true;
            DeleteDate = DateTimeOffset.Now.ToLocalTime();
        }
    }
}
