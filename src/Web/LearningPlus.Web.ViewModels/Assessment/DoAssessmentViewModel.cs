using LearningPlus.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningPlus.Web.ViewModels.Assessment
{
   public class DoAssessmentViewModel
    {
        public string ChildId { get; set; }

        [Required]
        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [Required]
        [DisplayName("Оценка")]
        public double Mark { get; set; }

        [Required]
        [DisplayName("Забележка")]
        public string Notes { get; set; }

        [Required]
        [DisplayName("Предмет")]
        public Disciplines Discipline { get; set; }
    }
}
