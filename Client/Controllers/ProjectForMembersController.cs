using Core.Base;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class ProjectForMembersController : Controller
    {
        BaseLink get = new BaseLink();
        // GET: ProjectForMembers
        public ActionResult Index()
        {
            return View(LoadProjectForMember());
        }

        public JsonResult LoadProjectForMember()
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

    }
}