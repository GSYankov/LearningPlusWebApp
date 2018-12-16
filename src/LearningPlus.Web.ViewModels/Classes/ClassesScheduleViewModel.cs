using LearningPlus.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlus.Web.ViewModels.Classes
{
    public class ClassesScheduleViewModel
    {
        public string Id { get; set; }

        public DaysOfWeek DayOfWeek { get; set; }

        public TimeOfDay TimeOfDay { get; set; }

        public Disciplines Discipline { get; set; }

        public string StudentNamesShort { get; set; }

        public Room Room { get; set; }

    }
}
