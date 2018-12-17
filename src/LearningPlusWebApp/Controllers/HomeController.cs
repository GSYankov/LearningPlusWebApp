using System.Diagnostics;
using AutoMapper;
using LearningPlus.Web.Models;
using Microsoft.AspNetCore.Mvc;
using LerningPlus.Web.Services.NewsService.Contract;

namespace LearningPlus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILearningPlusNewsService newsService;

        public HomeController(ILearningPlusNewsService newsService)
        {
            this.newsService = newsService;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (this.User.IsInRole("Admin"))
                {
                    var model = this.newsService.GetAdminNews();

                    return View("IndexLoggedIn", model);
                }

                if (this.User.IsInRole("Teacher"))
                {
                    var model = this.newsService.GetTeacherNews();

                    return View("IndexLoggedIn", model);
                }

                if (this.User.IsInRole("Parent") || this.User.IsInRole("Child"))
                {
                    var model = this.newsService.GetParentChildNews();

                    return View("IndexLoggedIn", model);
                }

                return View("IndexLoggedIn");
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
