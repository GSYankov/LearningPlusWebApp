using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlus.Web.Controllers
{
    public class ClassesController : Controller
    {
        public IActionResult Schedule()
        {
            //TODO: Extract constans
            ViewBag.Days = new List<string>()
            {
                "Понеделник", "Вторник", "Сряда", "Четвъртък", "Петък", "Събота", "Неделя"
            };

            ViewBag.Hours = new List<string>()
            {
                "08:00",  "09:30", "11:00", "13:30", "15:00", "16:30", "18:00"
            };
            return View();
        }
    }
}
