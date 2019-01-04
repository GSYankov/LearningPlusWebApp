using LearningPlus.Models;
using LearningPlus.Web.ViewModels.Assessment;
using System;
using System.Collections.Generic;
using System.Text;

namespace LerningPlus.Web.Services.AssessmentService.Contract
{
    public interface IAssessmentService
    {
        MarkbookViewModel GetMarkbookById(string id);
        LearningPlusAssessment CreateAssessment(DoAssessmentViewModel model);
    }
}
