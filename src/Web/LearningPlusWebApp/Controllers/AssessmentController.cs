using LearningPlus.Web.ViewModels.Assessment;
using LerningPlus.Web.Services.AssessmentService.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlus.Web.Controllers
{
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

        public IActionResult CreateAssessment(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public IActionResult CreateAssessment(DoAssessmentViewModel model)
        {
            this.assessmentService.CreateAssessment(model);

            return RedirectToAction("Markbook", "Assessment", new { id = model.ChildId });
        }
    }
}
