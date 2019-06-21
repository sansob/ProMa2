﻿using DataAccess.Models;
using DataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public interface ITicketService
    {
        List<Ticket> Get();
        List<Ticket> GetSearch(string values);
        Ticket Get(int id);
        bool Insert(TicketVM ticketVM);
        bool Update(int id, TicketVM ticketVM);
        bool Delete(int id);
    }
}
