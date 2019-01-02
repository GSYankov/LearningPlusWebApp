using AutoMapper;
using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LearningPlus.Web.ViewModels.Homework;
using LerningPlus.Web.Services.BlobService.Contract;
using LerningPlus.Web.Services.ClassesService.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LearningPlus.Web.Controllers
{
    public class HomeworksController : Controller
    {
        private readonly IBlobService blobService;
        private readonly IRepository<LearningPlusHomeWork> homeworkRepo;
        private readonly IRepository<LearningPlusClass> classesRepo;
        private readonly IMapper mapper;
        private readonly UserManager<LearningPlusUser> userManager;
        private readonly IClassesService classesService;

        public HomeworksController(IBlobService blobService,
            IRepository<LearningPlusHomeWork> homeworkRepo,
            IRepository<LearningPlusClass> classesRepo,
            IMapper mapper,
            UserManager<LearningPlusUser> userManager,
            IClassesService classesService)
        {
            this.blobService = blobService;
            this.homeworkRepo = homeworkRepo;
            this.classesRepo = classesRepo;
            this.mapper = mapper;
            this.userManager = userManager;
            this.classesService = classesService;
        }

        [Authorize(Roles = "Child, Parent")]
        public IActionResult UploadHomework()
        {
            var classes = this.classesService.GetStudentClasses(HttpContext.User);
            var model = new HomeworkUploadViewModel
            {
                Courses = classes.Select(c => this.mapper.Map<HomeworkUploadGetViewModel>(c)).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Child, Parent")]
        public IActionResult UploadHomework(HomeworkUploadViewModel model)
        {
            var course = this.classesRepo.All().SingleOrDefault(c => c.Id.ToString() == model.CourseId);
            var fileName = $"HW_{course.Discipline}_{course.DayOfWeek}_{course.TimeOfDay}_{User.Identity.Name}_{DateTime.UtcNow}";
            var blobLink = GlobalConstants.BlobStorageUrl + this.blobService.BlobUpload(model.Homework, fileName);
            var student = userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();

            var homework = new LearningPlusHomeWork
            {
                BlobLink = blobLink,
                Course = course,
                Student = student
            };

            this.homeworkRepo.AddAsync(homework).GetAwaiter().GetResult();
            this.homeworkRepo.SaveChangesAsync().GetAwaiter().GetResult();

            return RedirectToAction("UploadHomework");
        }

        [Authorize(Roles = "Teacher, Admin")]
        public IActionResult Assessment(string id)
        {
            var homework = this.homeworkRepo.All()
                .Include(hw => hw.Student).Include(hw => hw.Course)
                .SingleOrDefault(hw => hw.Id.ToString() == id);

            var model = this.mapper.Map<HomeworkAssessmentViewModel>(homework);

            return View("AssessmentHomework", model);
        }

        [HttpPost]
        [Authorize(Roles = "Teacher, Admin")]
        public IActionResult Assessment(HomeworkAssessmentViewModel model)
        {
            var homework = this.homeworkRepo.All()
            .Include(hw => hw.Student).Include(hw => hw.Course)
            .SingleOrDefault(hw => hw.Id.ToString() == model.Id);

            homework.Resolutions = model.Resolutions;
            this.homeworkRepo.SaveChangesAsync().GetAwaiter().GetResult();

            return Redirect("/");
        }

        [Authorize(Roles = "Parent, Child")]
        public IActionResult Assessed(string id)
        {
            var homework = this.homeworkRepo.All()
                .Include(hw => hw.Student).Include(hw => hw.Course)
                .SingleOrDefault(hw => hw.Id.ToString() == id);

            var model = this.mapper.Map<HomeworkAssessmentViewModel>(homework);

            return View("AssessedHomework", model);
        }
    }
}
