using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BusinessLogic.Service;
using DataAccess.Context;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace API.Controllers
{
    public class RepliesController : ApiController
    {
        private ApplicationContext db = new ApplicationContext();
        public RepliesController() { }
        private readonly IReplyService _iReplyService;

        public RepliesController(IReplyService iReplyService)
        {
            _iReplyService = iReplyService;
        }

        // GET: api/Replies
        public List<Reply> GetReplies()
        {
            return _iReplyService.Get();

        }

        // GET: api/Replies/5

        public Reply GetReply(int id)
        {
            return _iReplyService.Get(id);
        }

        // PUT: api/Replies/5


        // POST: api/Replies

        public void InsertReplies(ReplyVM replyVM)
        {
            _iReplyService.Insert(replyVM);
        }




        // DELETE: api/Replies/5
    }
}