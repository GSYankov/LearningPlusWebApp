using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public string Discipline { get; set; }
    }
}
