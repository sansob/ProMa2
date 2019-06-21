using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace BusinessLogic.Service.Application
{
    public class ReplyService : IReplyService
    {
        public ReplyService() { }
        private readonly IReplyRepository _iReplyRepository;

        public ReplyService(IReplyRepository iReplyRepository)
        {
            _iReplyRepository = iReplyRepository;
        }

        readonly bool status = false;

        public bool Delete(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return status;
            }
            else
            {
                return _iReplyRepository.Delete(id); ;
            }
            
        }

        public List<Reply> Get()
        {
            return _iReplyRepository.Get();
        }

        public Reply Get(int id)
        {
            return _iReplyRepository.Get(id);
        }


        public bool Insert(ReplyVM replyVM)
        {
            if (string.IsNullOrWhiteSpace(replyVM.ReplyFrom.ToString()) && string.IsNullOrWhiteSpace(replyVM.Ticket_Id.ToString()))
            {
                return status;
            }
            else
            {
                return _iReplyRepository.Insert(replyVM);
            }
        }

        public bool Update(int id, ReplyVM replyVM)
        {
            if (string.IsNullOrWhiteSpace(replyVM.Id.ToString()) && string.IsNullOrWhiteSpace(replyVM.Ticket_Id.ToString()))
            {
                return status;
            }
            else
            {
                return _iReplyRepository.Update(id, replyVM);
            }
                
        }

        public List<Reply> GetSearch(string values)
        {
            return _iReplyRepository.GetSearch(values);
        }
    }
}
