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

            var model = classesService.GetScheduleClasses();

            return View(model);
        }

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Create(string day = null, string hour = null)
        {
            CreateEditViewBag();
            ViewBag.SelectedDay = day;
            ViewBag.SelectedHour = hour;

            return View();
        }

        private void CreateEditViewBag()
        {
            ViewBag.Days = GlobalConstants.DaysOfWeek;
            ViewBag.FullHours = GlobalConstants.FullHours;
            ViewBag.Disciplines = (Disciplines[])Enum.GetValues(typeof(Disciplines));
            ViewBag.Rooms = (Room[])Enum.GetValues(typeof(Room));
            ViewBag.Teachers = GetUserIdsAndFullName("Teacher");
            ViewBag.Children = GetUserIdsAndFullName("Child");
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
            if (ModelState.IsValid)
            {
                this.classesService.Create(model);
                return RedirectToAction("Schedule", "Classes");
            }

            return RedirectToAction("Schedule", "Classes");
        }

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Details(string id)
        {
            var model = this.classesService.GetDetailsById(id);

            return View(model);
        }

        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Edit(ClassesCreateViewModel model)
        {
            //TODO: Implement editing
            CreateEditViewBag();


            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Teacher")]
        public IActionResult Delete(string id)
        {
            this.classesService.DeleteById(id);

            return RedirectToAction("Schedule", "Classes");
        }

    }
}
