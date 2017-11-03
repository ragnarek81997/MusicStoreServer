using System.Web.Mvc;

namespace MusicStoreServer.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}