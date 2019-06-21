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
    public class RulesController : Controller
    {
        BaseLink get = new BaseLink();

        // GET: Rules
        public ActionResult Index()
        {
            return View(LoadRule());
        }

        public JsonResult LoadRule()
        {
            IEnumerable<RuleVM> ruleVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Rules");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<RuleVM>>();
                readTask.Wait();
                ruleVM = readTask.Result;
            }
            else
            {
                ruleVM = Enumerable.Empty<RuleVM>();
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(ruleVM, JsonRequestBehavior.AllowGet);
        }
    }
}