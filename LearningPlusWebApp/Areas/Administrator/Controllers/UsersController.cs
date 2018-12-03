using AutoMapper;
using LearningPlusWebApp.Areas.Administrator.ViewModels;
using LearningPlusWebApp.Data;
using LearningPlusWebApp.Models;
using LearningPlusWebApp.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
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

        public async Task<IActionResult> RoleControl()
        {
            //TODO implement automapper

            var currentUser = await GetCurrentUserAsync();
            var model = this.db.Users.Where(u => u != currentUser).Select(u => new UsersListViewModel
            {
                Id = u.Id,
                Email = u.Email,
                UserName = u.UserName
            }).ToList();

            foreach (var user in model)
            {
                var realUser = await this.userManager.FindByIdAsync(user.Id);
                foreach (UserRoles role in (UserRoles[])Enum.GetValues(typeof(UserRoles)))
                {
                    if (await this.userManager.IsInRoleAsync(realUser, role.ToString()))
                    {
                        user.Role = role.ToString();
                    }
                }

            }

            ViewBag.Tabs = new Dictionary<string, string>() {
                { "tabs-1", "Admin" }, { "tabs-2", "Teacher" }, { "tabs-3", "Parent" }, { "tabs-4", "Child" }
            };

            return View(model);
        }

        private Task<LearningPlusUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string userId, string currentrole, string targetRole)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            await this.userManager.RemoveFromRoleAsync(user, currentrole);
            await this.userManager.AddToRoleAsync(user, targetRole);
            return RedirectToAction("RoleControl");
        }

        //[HttpPost]
        //public async Task<IActionResult> Promote(string userId)
        //{
        //    var user = await this.userManager.FindByIdAsync(userId);
        //    await this.userManager.RemoveFromRoleAsync(user, "User");
        //    await this.userManager.AddToRoleAsync(user, "Admin");
        //    return RedirectToAction("UsersList");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Demote(string userId)
        //{
        //    var user = await this.userManager.FindByIdAsync(userId);
        //    await this.userManager.RemoveFromRoleAsync(user, "Admin");
        //    await this.userManager.AddToRoleAsync(user, "User");
        //    return RedirectToAction("UsersList");
        //}
    }
}
