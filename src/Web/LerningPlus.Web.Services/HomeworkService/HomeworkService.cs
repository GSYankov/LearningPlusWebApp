using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LerningPlus.Web.Services.HomeworkService.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace LerningPlus.Web.Services.HomeworkService
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IRepository<LearningPlusHomeWork> hwRepo;
        private readonly UserManager<LearningPlusUser> userManager;

        public HomeworkService(IRepository<LearningPlusHomeWork> hwRepo,
            UserManager<LearningPlusUser> userManager)
        {
            this.hwRepo = hwRepo;
            this.userManager = userManager;
        }

        public ICollection<LearningPlusHomeWork> GetTeacherHomeworks(ClaimsPrincipal user)
        {
            var lpUser = userManager.GetUserAsync(user).GetAwaiter().GetResult();
            var homeworks = this.hwRepo.All().Where(h => h.Course.Teacher == lpUser).Include(h => h.Student).Include(h => h.Student).ToList();

            return homeworks;
        }

        public ICollection<LearningPlusHomeWork> GetStudentHomeworks(ClaimsPrincipal user)
        {
            var lpUser = userManager.GetUserAsync(user).GetAwaiter().GetResult();
            var homeworks = this.hwRepo.All().Where(h => h.Student == lpUser).ToList();

            return homeworks;
        }
    }
}
