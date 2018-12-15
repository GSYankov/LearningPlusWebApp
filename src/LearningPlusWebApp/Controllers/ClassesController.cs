using LearningPlus.Models;
using LearningPlus.Models.Enums;
using LearningPlus.Web.ViewModels.Classes;
using LerningPlus.Web.Services.ClassesService.Contract;
using LerningPlus.Web.Services.UsersService.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlus.Web.Controllers
{
    public class ClassesController : Controller
    {
        private readonly IUsersService usersService;
        private readonly UserManager<LearningPlusUser> userManager;
        private readonly IClassesService classesService;

        public ClassesController(IUsersService usersService,
            UserManager<LearningPlusUser> userManager,
            IClassesService classesService)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.classesService = classesService;
        }

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Schedule()
        {
            ViewBag.Days = GlobalConstants.DaysOfWeek;
            ViewBag.Hours = GlobalConstants.StartHours;

            return View();
        }

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Create()
        {
            ViewBag.Days = GlobalConstants.DaysOfWeek;
            ViewBag.FullHours = GlobalConstants.FullHours;
            ViewBag.Disciplines = (Disciplines[])Enum.GetValues(typeof(Disciplines));
            ViewBag.Rooms = (Room[])Enum.GetValues(typeof(Room));
            ViewBag.Teachers = GetUserIdsAndFullName("Teacher");
            ViewBag.Children = GetUserIdsAndFullName("Child");

            return View();
        }

        private Dictionary<string, string> GetUserIdsAndFullName(string role)
        {
            var usersIds = this.usersService.GetAllUserIdsInRole(role);
            var listOfUsers = new Dictionary<string, string>();

            foreach (var id in usersIds)
            {
                var teacher = this.userManager.FindByIdAsync(id).GetAwaiter().GetResult();
                listOfUsers.Add(teacher.Id, $"{teacher.FirstName} {teacher.LastName}");
            }

            return listOfUsers;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Create(ClassesCreateViewModel model)
        {
            this.classesService.Create(model);
            return RedirectToAction("Schedule", "Classes");
        }
    }
}
