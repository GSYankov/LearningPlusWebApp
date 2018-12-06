using AutoMapper;
using LearningPlusWebApp.Areas.Teacher.ViewModels;
using LearningPlusWebApp.Data;
using LearningPlusWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "Teacher")]
    public class UsersController : Controller
    {
        private readonly LearningPlusDbContext db;
        private readonly IMapper mapper;
        private readonly UserManager<LearningPlusUser> userManager;

        public UsersController(LearningPlusDbContext db, IMapper mapper, UserManager<LearningPlusUser> userManager)
        {
            //TODO: Use the Db as repository through service

            this.db = db;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public IActionResult UserApprovals()
        {
            //TODO implement automapper

            var userIdsWithRole = this.db.UserRoles.Select(ur => ur.UserId).ToList();
            var model = db.Users
                .Where(u => !userIdsWithRole.Contains(u.Id) && u.Children.Count > 0)
                .Select(u => new UserApprovalViewModel
                {
                    ParentId = u.Id,
                    ParentName = $"{u.FirstName} {u.LastName}",
                    ChildId = u.Children.FirstOrDefault().Id,
                    ChildName = $"{u.Children.FirstOrDefault().FirstName} {u.Children.FirstOrDefault().LastName}"
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveUsers(string parentId, string childId)
        {
            var parent = await this.userManager.FindByIdAsync(parentId);
            var child = await this.userManager.FindByIdAsync(childId);

            await this.userManager.AddToRoleAsync(parent, "Parent");
            await this.userManager.AddToRoleAsync(child, "Child");

            return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var model = this.mapper.Map<UserDetailsViewModel>(user);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var model = this.mapper.Map<UserDetailsViewModel>(user);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailsViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;

            this.db.SaveChanges();

            return Redirect("/");
        }
    }
}
