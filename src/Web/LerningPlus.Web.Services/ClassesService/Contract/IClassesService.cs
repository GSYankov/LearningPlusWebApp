using LearningPlus.Models;
using LearningPlus.Web.ViewModels.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LerningPlus.Web.Services.ClassesService.Contract
{
   public interface IClassesService
    {
        LearningPlusClass Create(ClassesCreateViewModel model);
        ICollection<ClassesScheduleViewModel> GetScheduleClasses();
    }
}
