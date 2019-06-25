using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base;
using DataAccess.ViewModels;

namespace DataAccess.Models
{
    [Table("TB_M_Tickets")]
    public class Ticket : BaseModel
    {
        [ForeignKey("Status")]
        public int Status_Id { get; set; }
        [ForeignKey("ProjectMember")]
        public int FromMember_Id { get; set; }
        [ForeignKey("Project")]
        public int Project_Id { get; set; }
        public string Message { get; set; }
        public DateTimeOffset Date { get; set;}

        public Status Status { get; set; }
        public ProjectMember ProjectMember { get; set; }
        public Project Project { get; set; }
        public Ticket() { }


        public Ticket (TicketVM ticketVM)
        {            
            Message = ticketVM.Message;
            Date = ticketVM.Date;
            CreateDate = DateTimeOffset.Now.ToLocalTime();
        }

        public void Update(int id, TicketVM ticketVM)
        {
            Message = ticketVM.Message;
            Date = ticketVM.Date;
            UpdateDate = DateTimeOffset.Now.ToLocalTime();
        }

        public void Delete()
        {
            IsDelete = true;
            DeleteDate = DateTimeOffset.Now.ToLocalTime();
        }
    }
}
