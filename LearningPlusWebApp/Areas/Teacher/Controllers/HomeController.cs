using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlus.Web.Areas.Teacher.Controllers
{
    public class HomeController : Controller
    {
        [Area("Teacher")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return Redirect("/");
        }
    }
}
