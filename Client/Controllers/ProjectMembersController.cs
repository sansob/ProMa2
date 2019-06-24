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
            IEnumerable<ProjectMemberVM> projectMemberVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("ProjectMembers");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ProjectMemberVM>>();
                readTask.Wait();
                projectMemberVM = readTask.Result;
            }
            else
            {
                projectMemberVM = Enumerable.Empty<ProjectMemberVM>();
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(projectMemberVM, JsonRequestBehavior.AllowGet);
        }

        public void InsertOrUpdate(ProjectMemberVM projectMemberVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var applicationContent = JsonConvert.SerializeObject(projectMemberVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(applicationContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            if (projectMemberVM.Id.Equals(0))
            {
                var result = client.PostAsync("ProjectMembers", byteContent).Result;
            }
            else
            {
                var result = client.PutAsync("ProjectMembers/" + projectMemberVM.Id, byteContent).Result;
            }
        }

        public JsonResult GetById(int id)
        {
            ProjectMemberVM projectMemberVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("ProjectMembers/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<ProjectMemberVM>();
                readTask.Wait();
                projectMemberVM = readTask.Result;
            }
            else
            {
                // try to find something
                projectMemberVM = null;
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(projectMemberVM, JsonRequestBehavior.AllowGet);
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