using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Models;
using WebUI.Models.Users;

namespace WebUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly AnalyzerContext _context;

        public UsersController(AnalyzerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.Select(p =>
                new UsersIndexListingModels {ChatId = p.ChatId, IsSubscribed = p.IsSubscribed});
            var model = new WebUI.Models.Users.UsersIndexModel { Users = users};
            return View(model);
        }
    }
}