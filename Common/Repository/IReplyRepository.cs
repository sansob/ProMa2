using DataAccess.Models;
using DataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repository
{
    public interface IReplyRepository
    {
        List<Reply> GetStatusByTicketId(int ticket_id);
        List<Reply> Get();
        List<Reply> GetSearch(string values);
        Reply Get(int id);
        bool Insert(ReplyVM replyVM);
        bool Update(int id, ReplyVM replyVM);
        bool Delete(int id);
    }
}
