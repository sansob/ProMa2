using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    

    public class RepliesController : Controller
    {
        BaseLink get = new BaseLink();

        // GET: Replies
        public ActionResult Index()
        {
            return View();
        }



        
    }
}