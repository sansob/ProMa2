using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class TicketVM
    {
        public int Id { get; set; }
        public int Status_Id { get; set; }
        public int FromMember_Id{ get; set; }
        public int Project_Id { get; set; }
        public string Message { get; set; }
        public DateTimeOffset Date { get; set; }

        public Status Status { get; set; }
        public ProjectMember ProjectMember { get; set; }
        public Project Project { get; set; }
        public TicketVM() { }

        public TicketVM(int status_Id, int fromMember_Id, int project_Id, string message, DateTimeOffset date)
        {
            this.Status_Id = status_Id;
            this.FromMember_Id = fromMember_Id;
            this.Project_Id = project_Id;
            this.Message = message;
            this.Date = date;
        }

        public void Update(int id, int status_Id, int fromMember_Id, int project_Id, string message, DateTimeOffset date)
        {
            this.Id = id;
            this.Status_Id = status_Id;
            this.FromMember_Id = fromMember_Id;
            this.Project_Id = project_Id;
            this.Message = message;
            this.Date = date;
        }
    }
}
