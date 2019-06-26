using System.Web.Mvc;

namespace Client.Controllers.Member
{
    public class ProjectDetailController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int Id)
        {
            return View();
        }
        
    }
}