using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using Core.Base;
using DataAccess.Models;
using Newtonsoft.Json;

namespace Client.Controllers
{
    [Authorize]
    public class StatusController : Controller
    {
        BaseLink get = new BaseLink();
        // GET
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult LoadStatus()
        {
            IEnumerable<Status> statuses = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Status");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Status>>();
                readTask.Wait();
                statuses = readTask.Result;
            }
            else
            {
                statuses = Enumerable.Empty<Status>();
                ModelState.AddModelError(string.Empty, "Server error");
            }

            return Json(statuses, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetStatusProject()
        {
            IEnumerable<Status> statuses = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Status?modulQuery=project");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Status>>();
                readTask.Wait();
                statuses = readTask.Result;
            }
            else
            {
                statuses = Enumerable.Empty<Status>();
                ModelState.AddModelError(string.Empty, "Server error");
            }

            return Json(statuses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStatusTask()
        {
            IEnumerable<Status> statuses = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Status?modulQuery=task");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Status>>();
                readTask.Wait();
                statuses = readTask.Result;
            }
            else
            {
                statuses = Enumerable.Empty<Status>();
                ModelState.AddModelError(string.Empty, "Server error");
            }

            return Json(statuses, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTicketStatus()
        {
            IEnumerable<Status> tickets = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Status?modulQuery=ticket");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Status>>();
                readTask.Wait();
                tickets = readTask.Result;
            }
            else
            {
                tickets = Enumerable.Empty<Status>();
                ModelState.AddModelError(string.Empty, "Server error");
            }

            return Json(tickets, JsonRequestBehavior.AllowGet);
        }



        public void InsertOrUpdate(Status status)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var myContent = JsonConvert.SerializeObject(status);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (status.Id.Equals(0))
            {
                var result = client.PostAsync("Status", byteContent).Result;
            }
            else
            {
                var result = client.PutAsync("Status/" + status.Id, byteContent).Result;
            }
        }
        
        public void Delete(int id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var result = client.DeleteAsync("Status/" + id).Result;
        }
        
        public JsonResult GetById(int id)
        {
            Status status = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Status/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Status>();
                readTask.Wait();
                status = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

    }
}