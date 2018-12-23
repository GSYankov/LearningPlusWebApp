using LearningPlus.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlus.Web.ViewModels.Classes
{
    public class ClassesScheduleViewModel
    {
        public string Id { get; set; }

        public string DayOfWeek { get; set; }

        public string TimeOfDay { get; set; }

        public string Discipline { get; set; }

        public string StudentNamesShort { get; set; }

        public string Room { get; set; }

    }
}
