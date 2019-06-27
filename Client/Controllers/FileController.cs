using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Mvc;
using Core.Base;
using DataAccess.Models;
using Newtonsoft.Json;

namespace Client.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly BaseLink get = new BaseLink();

        // GET: Projects
        public ActionResult Index()
        {
            return View(LoadFiles());
        }

        public JsonResult LoadFiles()
        {
            IEnumerable<File> files = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("File");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<File>>();
                readTask.Wait();
                files = readTask.Result;
            }
            else
            {
                files = Enumerable.Empty<File>();
                ModelState.AddModelError(string.Empty, "Server error");
            }

            return Json(files, JsonRequestBehavior.AllowGet);
        }        
        public JsonResult LoadFilesFromProject(int id)
        {
            IEnumerable<File> files = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("GetterFile/"+ id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<File>>();
                readTask.Wait();
                files = readTask.Result;
            }
            else
            {
                files = Enumerable.Empty<File>();
                ModelState.AddModelError(string.Empty, "Server error");
            }

            return Json(files, JsonRequestBehavior.AllowGet);
        }

        public void InsertOrUpdate(File file)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var myContent = JsonConvert.SerializeObject(file);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (file.Id.Equals(0))
            {
                var result = client.PostAsync("File", byteContent).Result;
            }
            else
            {
                var result = client.PutAsync("File/" + file.Id, byteContent).Result;
            }
        }

        public void Delete(int id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var result = client.DeleteAsync("File/" + id).Result;
        }

        public JsonResult GetById(int id)
        {
            File file = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(get.link)
            };
            var responseTask = client.GetAsync("File/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<File>();
                readTask.Wait();
                file = readTask.Result;
            }

            return Json(file, JsonRequestBehavior.AllowGet);
        }
    }
}