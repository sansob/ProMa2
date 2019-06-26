﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using DataAccess.Models;
using DataAccess.ViewModels;

namespace Common.Repository.Application
{
    public class ReplyRepository : IReplyRepository
    {
        ApplicationContext applicationContext = new ApplicationContext();
        bool status = false;

        public bool Delete(int id)
        {
            var get = Get(id);
            if (get != null)
            {
                get.Delete();
                applicationContext.Entry(get).State = System.Data.Entity.EntityState.Modified;
                applicationContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Reply> Get()
        {
            var get = applicationContext.Replies.Where(x => x.IsDelete == false).ToList();
            return get;
        }

        public Reply Get(int id)
        {
            var get = applicationContext.Replies.Find(id);
            return get;
        }

        public List<Reply> GetSearch(string values)
        {
            var get = applicationContext.Replies.Include("Ticket").Where
                (x => (x.Id.ToString().Contains(values) || x.Ticket.Status.Status_name.Contains(values)) &&
                x.IsDelete == false).ToList();
            return get;
        }

        public List<Reply> GetStatusByTicketId(int ticket_id)
        {
            var get = applicationContext.Replies.Include("Ticket").Where
                (x => x.Ticket_Id.Equals(ticket_id) &&
                x.IsDelete == false).ToList();
            return get;
        }

        public bool Insert(ReplyVM replyVM)
        {
            var push = new Reply(replyVM);
            var getTicket = applicationContext.Tickets.Find(replyVM.Ticket_Id);            
            if(getTicket != null)
            {
                push.Ticket = getTicket;
                applicationContext.Replies.Add(push);
                var result = applicationContext.SaveChanges();
                if (result > 0)
                {
                    status = true;
                    return status;
                }
                else
                {
                    return status;
                }
            }
            else
            {
                return status;
            }
           
            
        }

        public bool Update(int id, ReplyVM replyVM)
        {
            var pull = Get(id);
            var getTicket = applicationContext.Tickets.Find(replyVM.Ticket_Id);
            pull.Ticket = getTicket;
            pull.Update(id, replyVM);
            applicationContext.Entry(pull).State = EntityState.Modified;
            var result = applicationContext.SaveChanges();
            if (result > 0)
            {
                status = true;
                return status;
            }
            else
            {
                return status;
            }
        }
    
        
    }
}
