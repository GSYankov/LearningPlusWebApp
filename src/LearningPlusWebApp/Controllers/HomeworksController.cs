using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LearningPlus.Web.ViewModels.Homework;
using LerningPlus.Web.Services.BlobService.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlus.Web.Controllers
{
    public class HomeworksController : Controller
    {
        private readonly IBlobService blobService;
        private readonly IRepository<LearningPlusClass> classesRepo;
        private readonly IRepository<LearningPlusUser> usersRepo;
        private readonly IRepository<LearningPlusHomeWork> homeworkRepo;
        private readonly Microsoft.AspNetCore.Identity.UserManager<LearningPlusUser> userManager;

        public HomeworksController(IBlobService blobService,
            IRepository<LearningPlusClass> classesRepo,
            IRepository<LearningPlusUser> usersRepo,
            IRepository<LearningPlusHomeWork> homeworkRepo,
            UserManager<LearningPlusUser> userManager)
        {
            this.blobService = blobService;
            this.classesRepo = classesRepo;
            this.usersRepo = usersRepo;
            this.homeworkRepo = homeworkRepo;
            this.userManager = userManager;
        }

        public IActionResult Upload()
        {
            var users = usersRepo.All().Include(u => u.ClassesEnrolled).ToList();
            var currUserClasses = userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult().ClassesEnrolled.ToList();
            ViewBag.Classes = currUserClasses.Select(c => this.classesRepo.All().SingleOrDefault(x => x.Id == c.ClassId)).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Upload(HomeworkUploadViewModel model)
        {
            var blobLink = GlobalConstants.BlobStorageUrl + this.blobService.BlobUpload(model.Homework);
            var course = this.classesRepo.All().SingleOrDefault(c => c.Id.ToString() == model.CourseId);
            var student = userManager.GetUserAsync(HttpContext.User).GetAwaiter().GetResult();

            var homework = new LearningPlusHomeWork
            {
                BlobLink = blobLink,
                Course = course,
                Student = student
            };

            this.homeworkRepo.AddAsync(homework).GetAwaiter().GetResult();
            this.homeworkRepo.SaveChangesAsync().GetAwaiter().GetResult();

            return RedirectToAction("Upload");
        }
    }
}
