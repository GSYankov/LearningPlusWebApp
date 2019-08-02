using System.Diagnostics;
using AutoMapper;
using LearningPlus.Web.Models;
using Microsoft.AspNetCore.Mvc;
using LerningPlus.Web.Services.NewsService.Contract;
using LearningPlus.Web.ViewModels.Home;
using System.Collections.Generic;
using LearningPlus.Web.ViewModels;
using LerningPlus.Web.Services.ClassesService.Contract;
using System.Linq;
using LerningPlus.Web.Services.HomeworkService.Contract;
using System.Security.Claims;

namespace LearningPlus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILearningPlusNewsService newsService;
        private readonly IClassesService classesService;
        private readonly IHomeworkService homeworkService;
        private readonly IMapper mapper;

        public HomeController(ILearningPlusNewsService newsService,
            IClassesService classesService,
            IHomeworkService homeworkService,
            IMapper mapper)
        {
            this.newsService = newsService;
            this.classesService = classesService;
            this.homeworkService = homeworkService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var news = new List<NewsLoggedInViewModel>();
                var classes = new List<ClassesLoggedInViewModel>();
                var homeworks = new List<HomeworkLoggedInViewModel>();
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (this.User.IsInRole("Admin"))
                {
                    news = this.newsService.GetAdminNews();
                }

                if (this.User.IsInRole("Teacher"))
                {
                    news = this.newsService.GetTeacherNews();
                    var teacherClasses = this.classesService
                        .GetTeacherClasses(userId)
                        .OrderBy(c => c.DayOfWeek)
                        .ThenBy(c => c.TimeOfDay);

                    classes = teacherClasses.Select(uc => this.mapper.Map<ClassesLoggedInViewModel>(uc)).ToList();

                    var teacherHomeworks = this.homeworkService.GetTeacherHomeworksWithoutResolutions(userId);
                    homeworks = teacherHomeworks.Select(hw => this.mapper.Map<HomeworkLoggedInViewModel>(hw)).ToList();
                }

                if (this.User.IsInRole("Parent") || this.User.IsInRole("Child"))
                {
                    news = this.newsService.GetParentChildNews();
                    var studentClasses = this.classesService
                        .GetStudentClasses(HttpContext.User)
                        .OrderBy(c => c.DayOfWeek)
                        .ThenBy(c => c.TimeOfDay);

                    classes = studentClasses.Select(uc => this.mapper.Map<ClassesLoggedInViewModel>(uc)).ToList();

                    var studentHomeworks = this.homeworkService.GetStudentHomeworksWithResolutions(userId);
                    homeworks = studentHomeworks.Select(hw => this.mapper.Map<HomeworkLoggedInViewModel>(hw)).ToList();
                }

                var model = new LoggedInViewModel
                {
                    NewsViewModel = news,
                    ClassesViewModel = classes,
                    HomeworksViewModel = homeworks
                };

                return View("IndexLoggedIn", model);
            }

            return View();
        }

        public IActionResult BgLangAndLitr() => View("Educations/BgLangAndLitr");

        public IActionResult Math() => View("Educations/Math");

        public IActionResult History() => View("Educations/History");

        public IActionResult English() => View("Educations/English");

        public IActionResult Consults() => View("EducationalPortal/Consults");

        public IActionResult Profesion() => View("EducationalPortal/Profesion");

        public IActionResult Development() => View("EducationalPortal/Development");

        public IActionResult Gallery() => View("Gallery");



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
