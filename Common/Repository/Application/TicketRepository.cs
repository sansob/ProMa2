using System;
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
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationContext applicationContext = new ApplicationContext();
        private bool status = false;


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

        public List<Ticket> Get()
        {
            var get = applicationContext.Tickets.Include("Status").Include("ProjectMember.Rule").Include("Project").Where(x => x.IsDelete == false).ToList();
            return get;
        }

        public Ticket Get(int id)
        {
            var get = applicationContext.Tickets.Include("Status").Include("ProjectMember.Rule").Include("Project").SingleOrDefault(x => x.Id == id);
            return get;
        }

        public List<Ticket> GetSearch(string values)
        {
            var get = applicationContext.Tickets.Include("Status").Include("ProjectMember.Rule").Include("Project").Where
                (x => (x.Id.ToString().Contains(values) || x.Status.Status_name.Contains(values)) || x.Project.Project_name.Contains(values) &&
                x.IsDelete == false).ToList();
            return get;
        }

        public bool Insert(TicketVM ticketVM)
        {
            var push = new Ticket(ticketVM);
            var getStatus = applicationContext.Statuses.Find(ticketVM.Status_Id);
            var getProjectMember = applicationContext.ProjectMembers.Find(ticketVM.FromMember_Id);
            var getProject = applicationContext.Projects.Find(ticketVM.Project_Id);
            if(getStatus != null && getProjectMember != null && getProject != null)
            {
                push.Status = getStatus;
                push.ProjectMember = getProjectMember;
                push.Project = getProject;
                applicationContext.Tickets.Add(push);
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

        public bool Update(int id, TicketVM ticketVM)
        {

            var pull = Get(id);
            var getStatus = applicationContext.Statuses.Find(ticketVM.Status_Id);
            var getProjectMember = applicationContext.ProjectMembers.Find(ticketVM.FromMember_Id);
            var getProject = applicationContext.Projects.Find(ticketVM.Project_Id);
            if (getStatus != null && getProjectMember != null && getProject != null)
            {
                pull.Status = getStatus;
                pull.ProjectMember = getProjectMember;
                pull.Project = getProject;
                pull.Update(id, ticketVM);
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
            //var get = Get(id);
            //if (get != null)
            //{
            //    get.Update(id, ticketVM);
            //    applicationContext.Entry(get).State = EntityState.Modified;
            //    applicationContext.SaveChanges();
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

        }
    }
}
