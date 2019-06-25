using System;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Base;
using DataAccess.ViewModels;

namespace DataAccess.Models
{
    [Table("TB_M_Replies")]
    public class Reply : BaseModel
    {
        public int ReplyFrom { get; set; }
        public string Message { get; set; }
        public DateTimeOffset Date { get; set;  }
        [ForeignKey("Ticket")]
        public int Ticket_Id { get; set; }

        public Ticket Ticket { get; set; }
        public Reply() { }


        public Reply(ReplyVM replyVM)
        {
            ReplyFrom = replyVM.ReplyFrom;
            Message = replyVM.Message;
            Date = replyVM.Date;
            CreateDate = DateTimeOffset.Now.ToLocalTime();
        }

        public void Update(int id, ReplyVM replyVM)
        {
            ReplyFrom = replyVM.ReplyFrom;
            Message = replyVM.Message;
            Date = replyVM.Date;
            UpdateDate = DateTimeOffset.Now.ToLocalTime();
        }

        public void Delete()
        {
            IsDelete = true;
            DeleteDate = DateTimeOffset.Now.ToLocalTime();
        }

    }
}
