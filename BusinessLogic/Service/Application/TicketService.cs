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
    public class TicketService : ITicketService
    {
        public TicketService() {  }
        private readonly ITicketRepository _iTicketRepository;
        
        public TicketService(ITicketRepository iTicketRepository)
        {
            _iTicketRepository = iTicketRepository;
        }

        readonly bool status = false;

        public bool Delete(int id)
        {
            return _iTicketRepository.Delete(id);
        }

        public List<Ticket> Get()
        {
            return _iTicketRepository.Get();
        }

        public Ticket Get(int id)
        {
            return _iTicketRepository.Get(id);
        }

        public List<Ticket> GetSearch(string values)
        {
            return _iTicketRepository.GetSearch(values);
        }

        public bool Insert(TicketVM ticketVM)
        {
            if (string.IsNullOrWhiteSpace(ticketVM.Status_Id.ToString()) && string.IsNullOrWhiteSpace(ticketVM.FromMember_Id.ToString()) && string.IsNullOrWhiteSpace(ticketVM.Project_Id.ToString()))
            {
                return status;
            }
            else
            {
                return _iTicketRepository.Insert(ticketVM);
            }
        }

        public bool Update(int id, TicketVM ticketVM)
        {
            return _iTicketRepository.Update(id, ticketVM);
        }
    }
}
