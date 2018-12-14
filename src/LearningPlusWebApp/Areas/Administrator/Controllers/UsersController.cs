using AutoMapper;
using LearningPlus.Web.Areas.Administrator.ViewModels;
using LearningPlus.Models;
using LearningPlus.Web.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningPlus.Data.DbRepository.Contract;

namespace Eventures.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IRepository<LearningPlusUser> repository;
        private readonly UserManager<LearningPlusUser> userManager;

        public UsersController(IRepository<LearningPlusUser> repository, IMapper mapper, UserManager<LearningPlusUser> userManager)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<IActionResult> RoleControl()
        {
            //TODO implement automapper

            var currentUser = await GetCurrentUserAsync();
            var model = this.repository.All().Where(u => u != currentUser).Select(u => new UsersListViewModel
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
    }
}
