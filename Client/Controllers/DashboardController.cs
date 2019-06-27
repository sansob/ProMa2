using System.Web.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View();
        }
    }
}