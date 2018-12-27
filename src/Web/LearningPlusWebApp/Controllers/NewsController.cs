using AutoMapper;
using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LearningPlus.Web.ViewModels.News;
using LerningPlus.Web.Services.NewsService.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LearningPlus.Web.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private readonly ILearningPlusNewsService newsService;
        private readonly UserManager<LearningPlusUser> userManager;
        private readonly IRepository<LearningPlusNews> repository;
        private readonly IMapper mapper;

        public NewsController(ILearningPlusNewsService newsService, 
            UserManager<LearningPlusUser> userManager, 
            IRepository<LearningPlusNews> repository,
            IMapper mapper)
        {
            this.newsService = newsService;
            this.userManager = userManager;
            this.repository = repository;
            this.mapper = mapper;
        }

        public IActionResult Details(string id)
        {
            var news = this.newsService.GetById(id).FirstOrDefault();
            var model = this.mapper.Map<NewsDetailsViewModel>(news);

            return this.View(model);
        }

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Edit(string id)
        {
            var news = this.newsService.GetById(id).FirstOrDefault();
            var model = this.mapper.Map<NewsEditViewModel>(news);

            return View(model);
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        public IActionResult Edit(NewsCreateEditPostViewModel model)
        {
            var creator = userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
            this.newsService.EditNews(model, creator);

            return Redirect("/");
        }

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        public IActionResult Create(NewsCreateEditPostViewModel model)
        {
            var creator = userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();
            this.newsService.CreateNews(model, creator);

            return Redirect("/");
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpPost]
        public  IActionResult Delete (string id)
        {
            this.newsService.FakeDelete(id);

            return Redirect("/");
        }

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Archive()
        {
            //TODO: Improve news delete. Insert Pagination
            var model = this.newsService.GetArchivedNews();

            return View(model);
        }
    }
}
