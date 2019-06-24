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

namespace Client.Controllers
{
    public class TasksController : Controller
    {
        BaseLink get = new BaseLink();

        // GET: Tasks
        public ActionResult Index()
        {
            return View(LoadTask());
        }

        public JsonResult LoadTask()
        {
            IEnumerable<TaskVM> taskVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Tasks");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<TaskVM>>();
                readTask.Wait();
                taskVM = readTask.Result;
            }
            else
            {
                taskVM = Enumerable.Empty<TaskVM>();
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(taskVM, JsonRequestBehavior.AllowGet);
        }

        public void InsertOrUpdate(TaskVM taskVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var applicationContent = JsonConvert.SerializeObject(taskVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(applicationContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            if (taskVM.Id.Equals(0))
            {
                var result = client.PostAsync("Tasks", byteContent).Result;
            }
            else
            {
                var result = client.PutAsync("Tasks/" + taskVM.Id, byteContent).Result;
            }
        }

        public JsonResult GetById(int id)
        {
            TaskVM taskVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Tasks/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<TaskVM>();
                readTask.Wait();
                taskVM = readTask.Result;
            }
            else
            {
                // try to find something
                taskVM = null;
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(taskVM, JsonRequestBehavior.AllowGet);
        }

        public void Delete(int id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var result = client.DeleteAsync("Tasks/" + id).Result;
        }
    }
}