using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearningPlus.Web.Models;
using LearningPlus.Web.ViewModels.Home;
using AutoMapper;
using LearningPlus.Data.DbRepository;
using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LearningPlus.Web.ViewModels;

namespace LearningPlus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper mapper;
        private readonly IRepository<LearningPlusNews> newsRepo;

        public HomeController(IMapper mapper, IRepository<LearningPlusNews> repository)
        {
            this.mapper = mapper;
            this.newsRepo = repository;
        }

        public IActionResult Index()
        {
            //TODO: Create NewsService, Tests, CRUD on News
            if (this.User.Identity.IsAuthenticated)
            {
                if (this.User.IsInRole("Admin"))
                {
                    var model = this.newsRepo.All()
                        .Where(n => n.ExpiresOn > DateTime.UtcNow)
                        .Take(3)
                        .Select(n => this.mapper.Map<NewsLoggedInViewModel>(n))
                        .ToList();

                    return View("IndexLoggedIn", model);
                }

                if (this.User.IsInRole("Teacher"))
                {
                    var model = this.newsRepo.All()
                        .Where(n => n.ExpiresOn > DateTime.UtcNow
                                && n.TargetRoles.Any(tr => tr.TargetRole.ToString() != "Admin"))
                        .Take(3)
                        .Select(n => this.mapper.Map<NewsLoggedInViewModel>(n))
                        .ToList();

                    return View("IndexLoggedIn", model);
                }

                if (this.User.IsInRole("Teacher"))
                {
                    var model = this.newsRepo.All()
                        .Where(n => n.ExpiresOn > DateTime.UtcNow
                                && n.TargetRoles.Any(tr => tr.TargetRole.ToString() != "Admin"
                                                        && tr.TargetRole.ToString() != "Teacher"))
                        .Take(3)
                        .Select(n => this.mapper.Map<NewsLoggedInViewModel>(n))
                        .ToList();

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
