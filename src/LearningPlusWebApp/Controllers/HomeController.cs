﻿using System.Diagnostics;
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

                if (this.User.IsInRole("Admin"))
                {
                    news = this.newsService.GetAdminNews();
                }

                if (this.User.IsInRole("Teacher"))
                {
                    news = this.newsService.GetTeacherNews();
                    var teacherClasses = this.classesService
                        .GetTeacherClasses(HttpContext.User)
                        .OrderBy(c => c.DayOfWeek)
                        .ThenBy(c => c.TimeOfDay);

                    classes = teacherClasses.Select(uc => this.mapper.Map<ClassesLoggedInViewModel>(uc)).ToList();

                    var teacherHomeworks = this.homeworkService.GetTeacherHomeworks(HttpContext.User).Where(h => h.Resolutions is null).ToList();
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

                    var teacherHomeworks = this.homeworkService.GetStudentHomeworks(HttpContext.User).Where(h => h.Resolutions != null).ToList();
                    homeworks = teacherHomeworks.Select(hw => this.mapper.Map<HomeworkLoggedInViewModel>(hw)).ToList();
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
