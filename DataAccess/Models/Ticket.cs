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
            this.Message = ticketVM.Message;
            this.Date = ticketVM.Date;
            this.CreateDate = DateTimeOffset.Now.ToLocalTime();
        }

        public void Update(int id, TicketVM ticketVM)
        {
            this.Message = ticketVM.Message;
            this.Date = ticketVM.Date;
            this.UpdateDate = DateTimeOffset.Now.ToLocalTime();
        }

        public void Delete()
        {
            this.IsDelete = true;
            this.DeleteDate = DateTimeOffset.Now.ToLocalTime();
        }
    }
}
