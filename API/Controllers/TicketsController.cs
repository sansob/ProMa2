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
    public class TicketsController : ApiController
    {
        //private ApplicationContext db = new ApplicationContext();
        public TicketsController() { }
        private readonly ITicketService _iTicketService;

        public TicketsController(ITicketService iTicketService)
        {
            _iTicketService = iTicketService;
        }
        // GET: api/Tickets
        public HttpResponseMessage GetTickets()
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            var result = _iTicketService.Get();
            if (result != null)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return message; 
        }
        

        // GET: api/Tickets/5
        public HttpResponseMessage GetTicket(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            var result = _iTicketService.Get(id);
            if (result != null)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return message;
        }

        // PUT: api/Tickets/5

        public HttpResponseMessage PutUpdateTicket(int id, TicketVM ticketVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NotModified, "Not Modified");
            var result = _iTicketService.Update(id, ticketVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, ticketVM);
            }
            return message;
        }        

        // POST: api/Tickets
        public HttpResponseMessage InsertTicket(TicketVM ticketVM)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
            var result = _iTicketService.Insert(ticketVM);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK, ticketVM);
            }
            return message;
        }
        

        // DELETE: api/Tickets/5

        public HttpResponseMessage DeleteTicket(int id)
        {
            var message = Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Content");
            var result = _iTicketService.Delete(id);
            if (result)
            {
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            return message;
        }

    }
}