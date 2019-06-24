﻿using Core.Base;
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

        public void InsertOrUpdate(RuleVM ruleVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var applicationContent = JsonConvert.SerializeObject(ruleVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(applicationContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/Json");
            if (ruleVM.Id.Equals(0))
            {
                var result = client.PostAsync("Rules", byteContent).Result;
            }
            else
            {
                var result = client.PutAsync("Rules/" + ruleVM.Id, byteContent).Result;
            }
        }

        public JsonResult GetById(int id)
        {
            RuleVM ruleVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("Rules/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<RuleVM>();
                readTask.Wait();
                ruleVM = readTask.Result;
            }
            else
            {
                // try to find something
                ruleVM = null;
                ModelState.AddModelError(string.Empty, "Server error");
            }
            return Json(ruleVM, JsonRequestBehavior.AllowGet);
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