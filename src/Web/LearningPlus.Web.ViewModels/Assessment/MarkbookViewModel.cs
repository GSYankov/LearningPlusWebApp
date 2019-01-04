using LearningPlus.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningPlus.Web.ViewModels.Assessment
{
    public class MarkbookViewModel
    {
        public string ChildId { get; set; }

        public string ChildName { get; set; }

        public ICollection<AssessmentViewModel> Assessments { get; set; }
    }
}
