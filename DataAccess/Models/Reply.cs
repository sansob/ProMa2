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
            this.ReplyFrom = replyVM.ReplyFrom;
            this.Message = replyVM.Message;
            this.Date = replyVM.Date;
            this.CreateDate = DateTimeOffset.Now.ToLocalTime();
        }

        public void Update(int id, ReplyVM replyVM)
        {
            this.ReplyFrom = replyVM.ReplyFrom;
            this.Message = replyVM.Message;
            this.Date = replyVM.Date;
            this.UpdateDate = DateTimeOffset.Now.ToLocalTime();
        }

        public void Delete()
        {
            this.IsDelete = true;
            this.DeleteDate = DateTimeOffset.Now.ToLocalTime();
        }

    }
}
