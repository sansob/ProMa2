using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Core.Base;
using DataAccess.ViewModels;

namespace Client.Controllers {
    public class ProjectsController : Controller {

        BaseLink get = new BaseLink();

        // GET: Projects
        public ActionResult Index() {
            return View(LoadProject());
        }

        public JsonResult LoadProject() {
            IEnumerable<ProjectVM> projectVms = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Projects");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode) {
                var readTask = result.Content.ReadAsAsync<IList<ProjectVM>>();
                readTask.Wait();
                projectVms = readTask.Result;
            }
            else {
                projectVms = Enumerable.Empty<ProjectVM>();
                ModelState.AddModelError(string.Empty, "Server error");
            }

            return Json(projectVms, JsonRequestBehavior.AllowGet);
        }
    }
}