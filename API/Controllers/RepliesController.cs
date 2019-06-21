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
        public RepliesController() { }
        private readonly IReplyService _iReplyService;

        public RepliesController(IReplyService iReplyService)
        {
            _iReplyService = iReplyService;
        }

        // GET: api/Replies
        public HttpResponseMessage  GetReplies()
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            var result = _iReplyService.Get();
            if (result != null)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return message;           

        }

        // GET: api/Replies/5
        
        public HttpResponseMessage GetReply(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            var result = _iReplyService.Get(id);
            if (result != null)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return message;
            
        }

        // PUT: api/Replies/5
        public HttpResponseMessage PutUpdateReplies(int id, ReplyVM replyVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
            var result = _iReplyService.Update(id, replyVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, replyVM);
            }
            return message;
        }
        // POST: api/Replies

        public HttpResponseMessage InsertReplies(ReplyVM replyVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");            
            var result = _iReplyService.Insert(replyVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, replyVM);
            }
            return message;
        }

        // DELETE: api/Replies/5

        public HttpResponseMessage DeleteReplies(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Content");
            var result = _iReplyService.Delete(id);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            return message;
        }
    }
}