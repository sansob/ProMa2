using Core.Base;
using DataAccess.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using DataAccess.Models;

namespace Client.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        BaseLink get = new BaseLink();
        
        // GET: Tickets
        public ActionResult Index()
        {
            return View(LoadTicket());
        }

        public JsonResult LoadTicket()
        {
            IEnumerable<TicketVM> ticketVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Tickets");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<TicketVM>>();
                readTask.Wait();
                ticketVM = readTask.Result;
            }
            else
            {
                ticketVM = Enumerable.Empty<TicketVM>();
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(ticketVM, JsonRequestBehavior.AllowGet);

        }
        
        public JsonResult LoadTicketFromProject(int id)
        {
            IEnumerable<Ticket> files = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("GetterTicket/"+ id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Ticket>>();
                readTask.Wait();
                files = readTask.Result;
            }
            else
            {
                files = Enumerable.Empty<Ticket>();
                ModelState.AddModelError(string.Empty, "Server error");
            }

            return Json(files, JsonRequestBehavior.AllowGet);
        }

        public void InsertOrUpdate(TicketVM ticketVM)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(get.link);
            var myContent = JsonConvert.SerializeObject(ticketVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (ticketVM.Id.Equals(0))
            {
                var result = client.PostAsync("Tickets", byteContent).Result;
            }
            else
            {
                var result = client.PutAsync("Tickets/" + ticketVM.Id, byteContent).Result;
            }
        }


    }
}