using System.Collections.Generic;
using System.ComponentModel;

namespace LearningPlus.Web.ViewModels.Classes
{
    public   class ClassesCreateViewModel
    {
        [DisplayName("Ден от седмицата")]
        public string DayOfWeek { get; set; }

        [DisplayName("Време от деня")]
        public string TimeOfDay { get; set; }

        [DisplayName("Предмет")]
        public string Discipline { get; set; }

        [DisplayName("Ученици")]
        public ICollection<string> StudentIds { get; set; }

        [DisplayName("Учител")]
        public string TeacherId { get; set; }

        [DisplayName("Стая")]
        public string Room { get; set; }
    }
}
