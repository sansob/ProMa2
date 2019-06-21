using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class TaskVM
    {
        public TaskVM(int project_id, string description, DateTimeOffset start_date, DateTimeOffset due_date, int assigned_by_member, int priority, int status_id, int assigned_to_member)
        {
            this.Project_Id = project_id;
            this.Description = description;
            this.Start_Date = start_date;
            this.Due_Date = due_date;
            this.Assigned_By_Member = assigned_by_member;
            this.Priority = priority;
            this.Status_Id = status_id;
            this.Assigned_To_Member = assigned_to_member;
        }
        public TaskVM() { }
        public void Update(int id, int project_id, string description, DateTimeOffset start_date, DateTimeOffset due_date, int assigned_by_member, int priority, int status_id, int assigned_to_member)
        {
            this.Id = id;
            this.Project_Id = project_id;
            this.Description = description;
            this.Start_Date = start_date;
            this.Due_Date = due_date;
            this.Assigned_By_Member = assigned_by_member;
            this.Priority = priority;
            this.Status_Id = status_id;
            this.Assigned_To_Member = assigned_to_member;
        }
        public int Id { get; set; }
        public int Project_Id { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Start_Date { get; set; }
        public DateTimeOffset Due_Date { get; set; }
        public int Assigned_By_Member { get; set; }
        public int Priority { get; set; }
        public int Status_Id { get; set; }
        public int Assigned_To_Member { get; set; }
    }
}
