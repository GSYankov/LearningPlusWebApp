using LearningPlus.Models;
using LearningPlus.Web.ViewModels.Classes;
using System.Collections.Generic;
using System.Security.Claims;

namespace LerningPlus.Web.Services.ClassesService.Contract
{
    public interface IClassesService
    {
        LearningPlusClass Create(ClassesCreateViewModel model);

        ICollection<ClassesScheduleViewModel> GetScheduleClasses();

        ClassesDetailsViewModel GetDetailsById(string id);

        LearningPlusClass DeleteById(string id);

        ICollection<LearningPlusClass> GetStudentClasses(ClaimsPrincipal user);

        ICollection<LearningPlusClass> GetTeacherClasses(ClaimsPrincipal user);
    }
}
