using Core.Base;
using DataAccess.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            IEnumerable<Task> task = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Tasks");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Task>>();
                readTask.Wait();
                task = readTask.Result;
            }
            else
            {
                task = Enumerable.Empty<Task>();
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(task, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadTaskFromProject(int id)
        {
            IEnumerable<Task> files = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("GetterTask/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Task>>();
                readTask.Wait();
                files = readTask.Result;
            }
            else
            {
                files = Enumerable.Empty<Task>();
                ModelState.AddModelError(string.Empty, "Server error");
            }

            return Json(files, JsonRequestBehavior.AllowGet);
        }

        public void InsertOrUpdate(Task task)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var applicationContent = JsonConvert.SerializeObject(task);
            var buffer = System.Text.Encoding.UTF8.GetBytes(applicationContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            if (task.Id.Equals(0))
            {
                var result = client.PostAsync("Tasks", byteContent).Result;
            }
            else
            {
                var result = client.PutAsync("Tasks/" + task.Id, byteContent).Result;
            }
        }

        public JsonResult GetById(int id)
        {
            Task task = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Tasks/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Task>();
                readTask.Wait();
                task = readTask.Result;
            }
            else
            {
                // try to find something
                task = null;
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(task, JsonRequestBehavior.AllowGet);
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