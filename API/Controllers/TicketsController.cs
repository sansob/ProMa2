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
        private ApplicationContext db = new ApplicationContext();
        public TicketsController() { }
        private readonly ITicketService _iTicketService;

        public TicketsController(ITicketService iTicketService)
        {
            _iTicketService = iTicketService;
        }
        // GET: api/Tickets
        public List<Ticket> GetTickets()
        {
            return _iTicketService.Get();
        }

        // GET: api/Tickets/5
        public Ticket GetTicket(int id)
        {
            return _iTicketService.Get(id);
        }

        // POST: api/Tickets


        // PUT: api/Tickets/5

        public void InsertTicket(TicketVM ticketVM)
        {
            _iTicketService.Insert(ticketVM);
        }
        

        // DELETE: api/Tickets/5
        
    }
}