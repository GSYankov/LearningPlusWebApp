using LearningPlus.Models.Enums;
using LearningPlus.Web.ViewModels.Assessment;
using LerningPlus.Web.Services.AssessmentService.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;

namespace LearningPlus.Web.Controllers
{
    [Authorize]
    public class AssessmentController : Controller
    {
        private readonly IAssessmentService assessmentService;

        public AssessmentController(IAssessmentService assessmentService)
        {
            this.assessmentService = assessmentService;
        }

        public IActionResult Markbook(string id)
        {
            var model = assessmentService.GetMarkbookById(id);

            return View(model);
        }

        [Authorize(Roles = "Teacher")]
        public IActionResult CreateAssessment(string id)
        {
            ViewBag.Id = id;
            ViewBag.Disciplines = Enum.GetValues(typeof(Disciplines))
                .Cast<Disciplines>()
                .Select(d => new SelectListItem(d.ToString(), d.ToString()));

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public IActionResult CreateAssessment(DoAssessmentViewModel model)
        {
            this.assessmentService.CreateAssessment(model);

            return RedirectToAction("Markbook", new { id = model.ChildId });
        }
    }
}
