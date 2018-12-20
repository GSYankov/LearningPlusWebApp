using LearningPlus.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LerningPlus.Web.Services.HomeworkService.Contract
{
    public interface IHomeworkService
    {
        ICollection<LearningPlusHomeWork> GetTeacherHomeworks(System.Security.Claims.ClaimsPrincipal user);

        ICollection<LearningPlusHomeWork> GetStudentHomeworks(System.Security.Claims.ClaimsPrincipal user);
    }
}
