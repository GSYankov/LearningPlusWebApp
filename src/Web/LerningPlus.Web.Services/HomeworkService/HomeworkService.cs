using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LerningPlus.Web.Services.HomeworkService.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace LerningPlus.Web.Services.HomeworkService
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IRepository<LearningPlusHomeWork> hwRepo;

        public HomeworkService(IRepository<LearningPlusHomeWork> hwRepo)
        {
            this.hwRepo = hwRepo;
        }

        public ICollection<LearningPlusHomeWork> GetTeacherHomeworksWithoutResolutions(string teacherId)
        {
            var homeworks = this.hwRepo.All()
                .Where(h => h.Course.Teacher.Id == teacherId && string.IsNullOrEmpty(h.Resolutions))
                .Include(h => h.Student)
                .ToList();

            return homeworks;
        }

        public ICollection<LearningPlusHomeWork> GetStudentHomeworksWithResolutions(string studentId)
        {
            var homeworks = this.hwRepo.All()
                .Where(h => h.Student.Id == studentId && !string.IsNullOrEmpty(h.Resolutions))
                .ToList();

            return homeworks;
        }
    }
}
