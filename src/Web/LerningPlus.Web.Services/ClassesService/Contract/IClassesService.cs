using LearningPlus.Models;
using LearningPlus.Web.ViewModels.Classes;
using System.Collections.Generic;

namespace LerningPlus.Web.Services.ClassesService.Contract
{
    public interface IClassesService
    {
        LearningPlusClass Create(ClassesCreateViewModel model);

        ICollection<ClassesScheduleViewModel> GetScheduleClasses();

        ClassesDetailsViewModel GetDetailsById(string id);

        LearningPlusClass DeleteById(string id);
    }
}
