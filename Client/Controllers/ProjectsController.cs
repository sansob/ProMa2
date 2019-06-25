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
    public class ProjectsController : Controller
    {
        BaseLink get = new BaseLink();

        // GET: Projects
        public ActionResult Index()
        {
            return View(LoadProject());
        }

        public JsonResult LoadProject()
        {
            IEnumerable<Project> projectVms = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Projects");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Project>>();
                readTask.Wait();
                projectVms = readTask.Result;
            }
            else
            {
                projectVms = Enumerable.Empty<Project>();
                ModelState.AddModelError(string.Empty, "Server error");
            }

            return Json(projectVms, JsonRequestBehavior.AllowGet);
        }

        public void InsertOrUpdate(Project project)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var myContent = JsonConvert.SerializeObject(project);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (project.Id.Equals(0))
            {
                var result = client.PostAsync("Projects", byteContent).Result;
            }
            else
            {
                var result = client.PutAsync("Projects/" + project.Id, byteContent).Result;
            }
        }
        
        public void Delete(int id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var result = client.DeleteAsync("Projects/" + id).Result;
        }
        
        public JsonResult GetById(int id)
        {
            Project project = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Projects/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Project>();
                readTask.Wait();
                project = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(project, JsonRequestBehavior.AllowGet);
        }
        
    }
}