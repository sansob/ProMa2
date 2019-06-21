using Core.Base;
using DataAccess.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
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
    }
}