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
    public class ProjectMembersController : Controller
    {
        BaseLink get = new BaseLink();

        // GET: ProjectMembers
        public ActionResult Index()
        {
            return View(LoadProjectMember());
        }

        public JsonResult LoadProjectMember()
        {
            IEnumerable<ProjectMember> projectMember = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("ProjectMembers");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ProjectMember>>();
                readTask.Wait();
                projectMember = readTask.Result;
            }
            else
            {
                projectMember = Enumerable.Empty<ProjectMember>();
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(projectMember, JsonRequestBehavior.AllowGet);
        }

        public void InsertOrUpdate(ProjectMember projectMember)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var applicationContent = JsonConvert.SerializeObject(projectMember);
            var buffer = System.Text.Encoding.UTF8.GetBytes(applicationContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            if (projectMember.Id.Equals(0))
            {
                var result = client.PostAsync("ProjectMembers", byteContent).Result;
            }
            else
            {
                var result = client.PutAsync("ProjectMembers/" + projectMember.Id, byteContent).Result;
            }
        }

        public JsonResult GetById(int id)
        {
            ProjectMember projectMember = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("ProjectMembers/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<ProjectMember>();
                readTask.Wait();
                projectMember = readTask.Result;
            }
            else
            {
                // try to find something
                projectMember = null;
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(projectMember, JsonRequestBehavior.AllowGet);
        }

        public void Delete(int id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var result = client.DeleteAsync("ProjectMembers/" + id).Result;
        }
    }
}