﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlus.Web.Areas.Administrator.Controllers
{
    public class HomeController : Controller
    {
        [Area("Administrator")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return Redirect("/");
        }
    }
}
