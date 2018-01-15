using System.Linq;
using System.Web.Mvc;
using Models.Models;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new AnalyzerContext())
            {
                ViewBag.Users = db.Users.ToList();
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}