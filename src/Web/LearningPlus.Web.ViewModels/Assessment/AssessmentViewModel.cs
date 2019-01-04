using LearningPlus.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningPlus.Web.ViewModels.Assessment
{
   public class AssessmentViewModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Mark { get; set; }

        [Required]
        public string Notes { get; set; }

        [Required]
        public string Discipline { get; set; }
    }
}
