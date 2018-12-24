using LearningPlus.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LerningPlus.Web.Services.HomeworkService.Contract
{
    public interface IHomeworkService
    {
        ICollection<LearningPlusHomeWork> GetTeacherHomeworksWithoutResolutions(string teacherId);

        ICollection<LearningPlusHomeWork> GetStudentHomeworksWithResolutions(string studentId);
    }
}
