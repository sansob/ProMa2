using System.Web.Mvc;

namespace Client.Controllers
{
    public class DashboardController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}