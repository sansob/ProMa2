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
    [Authorize]
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
            IEnumerable<Rule> rule = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Rules");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Rule>>();
                readTask.Wait();
                rule = readTask.Result;
            }
            else
            {
                rule = Enumerable.Empty<Rule>();
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(rule, JsonRequestBehavior.AllowGet);
        }

        public void InsertOrUpdate(Rule rule)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var applicationContent = JsonConvert.SerializeObject(rule);
            var buffer = System.Text.Encoding.UTF8.GetBytes(applicationContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            if (rule.Id.Equals(0))
            {
                var result = client.PostAsync("Rules", byteContent).Result;
            }
            else
            {
                var result = client.PutAsync("Rules/" + rule.Id, byteContent).Result;
            }
        }

        public JsonResult GetById(int id)
        {
            Rule rule = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Rules/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Rule>();
                readTask.Wait();
                rule = readTask.Result;
            }
            else
            {
                // try to find something
                rule = null;
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(rule, JsonRequestBehavior.AllowGet);
        }

        public void Delete(int id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var result = client.DeleteAsync("Rules/" + id).Result;
        }
    }
}