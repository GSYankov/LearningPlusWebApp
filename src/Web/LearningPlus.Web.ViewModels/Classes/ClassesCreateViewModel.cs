using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearningPlus.Web.ViewModels.Classes
{
    public   class ClassesCreateViewModel
    {
        [Required]
        [DisplayName("Ден от седмицата")]
        public string DayOfWeek { get; set; }

        [Required]
        [DisplayName("Време от деня")]
        public string TimeOfDay { get; set; }

        [Required]
        [DisplayName("Предмет")]
        public string Discipline { get; set; }

        [Required(ErrorMessage ="Моля, изберете поне един ученик")]
        [DisplayName("Ученици")]
        public ICollection<string> StudentIds { get; set; }

        [Required]
        [DisplayName("Учител")]
        public string TeacherId { get; set; }

        [Required]
        [DisplayName("Стая")]
        public string Room { get; set; }
    }
}
