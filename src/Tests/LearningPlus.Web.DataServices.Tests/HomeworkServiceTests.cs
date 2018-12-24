using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LerningPlus.Web.Services.HomeworkService;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LearningPlus.Web.DataServices.Tests
{
    public class HomeworkServiceTests
    {
        [Fact]
        public void GetTeacherHomeworksWithoutResolutionsReturnsCorrectHomeworks()
        {
            PrepareData(out Mock<IRepository<LearningPlusHomeWork>> hwRepo, 
                out Mock<LearningPlusUser> student, 
                out Mock<LearningPlusClass> course);

            var expectedResult = new List<LearningPlusHomeWork>
            {
               new LearningPlusHomeWork{  Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"), Resolutions=null, Course = course.Object, Student = student.Object },
               new LearningPlusHomeWork{  Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"), Resolutions="", Course = course.Object, Student = student.Object},
               new LearningPlusHomeWork{  Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"), Resolutions="", Course = course.Object, Student = student.Object },
            };

            var expectedResultJson = JsonConvert.SerializeObject(expectedResult.Select(h => h.Id));

            var homeworkService = new HomeworkService(hwRepo.Object);

            var result = homeworkService.GetTeacherHomeworksWithoutResolutions("teacher");
            var resultJson = JsonConvert.SerializeObject(result.Select(h => h.Id));

            resultJson.ShouldBe(expectedResultJson);
        }

        [Fact]
        public void GetStudentHomeworksWithResolutionsReturnsCorrectHomeworks()
        {
            PrepareData(out Mock<IRepository<LearningPlusHomeWork>> hwRepo,
                out Mock<LearningPlusUser> student, 
                out Mock<LearningPlusClass> course);

            var expectedResult = new List<LearningPlusHomeWork>
            {
               new LearningPlusHomeWork{  Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"), Resolutions="test", Course = course.Object, Student = student.Object },
               new LearningPlusHomeWork{  Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"), Resolutions="test", Course = course.Object, Student = student.Object },
            };

            var expectedResultJson = JsonConvert.SerializeObject(expectedResult.Select(h => h.Id));
            var homeworkService = new HomeworkService(hwRepo.Object);

            var result = homeworkService.GetStudentHomeworksWithResolutions("student");
            var resultJson = JsonConvert.SerializeObject(result.Select(h => h.Id));

            resultJson.ShouldBe(expectedResultJson);

        }

        private static void PrepareData(out Mock<IRepository<LearningPlusHomeWork>> hwRepo,
            out Mock<LearningPlusUser> student,
            out Mock<LearningPlusClass> course)
        {
            hwRepo = new Mock<IRepository<LearningPlusHomeWork>>();
            student = new Mock<LearningPlusUser>();
            student.Setup(s => s.Id).Returns("student");

            var teacher = new Mock<LearningPlusUser>();
            teacher.Setup(t => t.Id).Returns("teacher");

            course = new Mock<LearningPlusClass>();
            course.Setup(c => c.Teacher).Returns(teacher.Object);

            hwRepo.Setup(r => r.All()).Returns(new List<LearningPlusHomeWork>
            {
               new LearningPlusHomeWork{  Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"), Resolutions=null, Course = course.Object, Student = student.Object },
               new LearningPlusHomeWork{  Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"), Resolutions="", Course = course.Object, Student = student.Object } ,
               new LearningPlusHomeWork{  Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"), Resolutions="test", Course = course.Object, Student = student.Object },
               new LearningPlusHomeWork{  Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF01"), Resolutions="", Course = course.Object, Student = student.Object },
               new LearningPlusHomeWork{  Id = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00"), Resolutions="test", Course = course.Object, Student = student.Object },
            }.AsQueryable());
        }
    }
}
