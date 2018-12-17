using LearningPlus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningPlus.Web.ViewModels.Classes
{
   public class ClassesDetailsViewModel
    {
        public string Id { get; set; }

        [DisplayName("Ден")]
        public string DayOfWeek { get; set; }

        [DisplayName("Час")]
        public string TimeOfDay { get; set; }

        [DisplayName("Предмет")]
        public string Discipline { get; set; }

        public ICollection<LearningPlusUser> Students { get; set; }

        [DisplayName("Преподавател")]
        public string Teacher { get; set; }

        [DisplayName("Стая")]
        public string Room { get; set; }
    }
}
