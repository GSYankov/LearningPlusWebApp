using AutoMapper;
using LearningPlusWebApp.Areas.Administrator.ViewModels;
using LearningPlusWebApp.Areas.Teacher.ViewModels;
using LearningPlusWebApp.Data;
using LearningPlusWebApp.Models;
using LearningPlusWebApp.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    }
}
