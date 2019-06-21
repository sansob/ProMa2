using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ViewModels
{
    public class ReplyVM
    {
        public int Id { get; set; }
        public int ReplyFrom { get; set; }
        public string Message { get; set; }
        public DateTimeOffset Date { get; set; }
        public int Ticket_Id { get; set; }

        public Ticket Ticket { get; set; }
        public ReplyVM() { }

        public ReplyVM(int replyFrom, string message, DateTimeOffset date, int ticket_Id)
        {
            this.ReplyFrom = replyFrom;
            this.Message = message;
            this.Date = date;
            this.Ticket_Id = ticket_Id;
        }

        public void Update (int replyFrom, string message, DateTimeOffset date, int ticket_Id)
        {
            this.ReplyFrom = replyFrom;
            this.Message = message;
            this.Date = date;
            this.Ticket_Id = ticket_Id;
        }
    }
}
