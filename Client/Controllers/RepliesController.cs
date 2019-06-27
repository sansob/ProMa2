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
    public class RepliesController : Controller
    {
        BaseLink get = new BaseLink();

        // GET: Replies
        public ActionResult Index()
        {
            return View(LoadReplies());
        }

        public JsonResult LoadReplies()
        {
            IEnumerable<Reply> replies = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Replies");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Reply>>();
                readTask.Wait();
                replies = readTask.Result;
            }
            else
            {
                replies = Enumerable.Empty<Reply>();
                ModelState.AddModelError(string.Empty, "Server error");
            }

            return Json(replies, JsonRequestBehavior.AllowGet);
        }

        public void Delete(int id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var result = client.DeleteAsync("Replies/" + id).Result;
        }
    }
}