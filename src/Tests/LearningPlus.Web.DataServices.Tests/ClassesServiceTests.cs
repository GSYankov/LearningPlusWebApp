using LearningPlus.Web.ViewModels.Classes;
using LearningPlus.Models.Enums;
using Xunit;
using System.Collections.Generic;
using Moq;
using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LearningPlus.Data;
using Microsoft.EntityFrameworkCore;
using LearningPlus.Data.DbRepository;
using LerningPlus.Web.Services.ClassesService;
using Shouldly;
using Microsoft.AspNetCore.Identity;

namespace LearningPlus.Web.DataServices.Tests
{
    public class ClassesServiceTests
    {
        [Fact]
        public void CreateReturnsClassInRepository()
        {
            var model = new ClassesCreateViewModel
            {
                DayOfWeek = DaysOfWeek.Понеделник.ToString(),
                Discipline = Disciplines.Английски_език.ToString(),
                Room = Room.Стая_1.ToString(),
                TimeOfDay = "11:00 - 12:30",
                TeacherId = "teacherId",
                StudentIds = new List<string> { "studentId" }
            };

            var options = new DbContextOptionsBuilder<LearningPlusDbContext>()
                 .UseInMemoryDatabase(databaseName: "Create_Classes_Database") // Give a Unique name to the DB
                 .Options;
            var dbContext = new LearningPlusDbContext(options);

            var repository = new DbRepository<LearningPlusClass>(dbContext);
            var classesService = new ClassesService(null, null, repository, null);

            var createdModel = classesService.Create(model);

            repository.All().CountAsync().GetAwaiter().GetResult().ShouldBe(1);
        }

        [Fact]
        public void DeleteByIdReturnsInactiveClass()
        {
            var teacher = new Mock<LearningPlusUser>();
            teacher.Setup(t => t.Id).Returns("teacherId");
            teacher.Setup(t => t.FirstName).Returns("TeacherFirst");
            teacher.Setup(t => t.LastName).Returns("TeacherLast");

            var model = new ClassesCreateViewModel
            {
                DayOfWeek = DaysOfWeek.Понеделник.ToString(),
                Discipline = Disciplines.Английски_език.ToString(),
                Room = Room.Стая_1.ToString(),
                TimeOfDay = "11:00 - 12:30",
                TeacherId = "teacherId",
                StudentIds = new List<string> { "studentId" }
            };

            var options = new DbContextOptionsBuilder<LearningPlusDbContext>()
                 .UseInMemoryDatabase(databaseName: "Delete_Classes_Database") // Give a Unique name to the DB
                 .Options;
            var dbContext = new LearningPlusDbContext(options);
            dbContext.Users.Add(teacher.Object);
            var repository = new DbRepository<LearningPlusClass>(dbContext);
            var classesService = new ClassesService(null, null, repository, null);

            var createdModel = classesService.Create(model);
            var id = createdModel.Id.ToString();
            var deletedClass = classesService.DeleteById(id);

            deletedClass.Active.ShouldBe(false);
        }

        [Fact]
        public void DetailsByIdReturnsCorrectClass()
        {
            var teacher = new Mock<LearningPlusUser>();
            teacher.Setup(t => t.Id).Returns("teacherId");
            teacher.Setup(t => t.FirstName).Returns("TeacherFirst");
            teacher.Setup(t => t.LastName).Returns("TeacherLast");

            var student = new Mock<LearningPlusUser>();
            student.Setup(t => t.Id).Returns("studentId");

            var model = new ClassesCreateViewModel
            {
                DayOfWeek = DaysOfWeek.Понеделник.ToString(),
                Discipline = Disciplines.Английски_език.ToString(),
                Room = Room.Стая_1.ToString(),
                TimeOfDay = "11:00 - 12:30",
                TeacherId = "teacherId",
                StudentIds = new List<string> { "studentId" }
            };

            var options = new DbContextOptionsBuilder<LearningPlusDbContext>()
                 .UseInMemoryDatabase(databaseName: "Details_Classes_Database") // Give a Unique name to the DB
                 .Options;
            var dbContext = new LearningPlusDbContext(options);
            dbContext.Users.Add(teacher.Object);
            dbContext.Users.Add(student.Object);
            var repository = new DbRepository<LearningPlusClass>(dbContext);
            var classesService = new ClassesService(null, null, repository, null);

            var createdModel = classesService.Create(model);
            var id = createdModel.Id.ToString();
            var detailsClass = classesService.GetDetailsById(id);

            detailsClass.Id.ToString().ShouldBe(id);
        }

        [Fact]
        public void GetScheduleClassesReturnsCorrectClasses()
        {
            var student = new Mock<LearningPlusUser>();
            student.Setup(t => t.Id).Returns("studentId");
            student.Setup(t => t.FirstName).Returns("First");
            student.Setup(t => t.LastName).Returns("Last");

            var model = new ClassesCreateViewModel
            {
                DayOfWeek = DaysOfWeek.Понеделник.ToString(),
                Discipline = Disciplines.Английски_език.ToString(),
                Room = Room.Стая_1.ToString(),
                TimeOfDay = "11:00 - 12:30",
                TeacherId = "teacherId",
                StudentIds = new List<string> { "studentId" }
            };

            var options = new DbContextOptionsBuilder<LearningPlusDbContext>()
                 .UseInMemoryDatabase(databaseName: "Schedule_Classes_Database") // Give a Unique name to the DB
                 .Options;
            var dbContext = new LearningPlusDbContext(options);
            dbContext.Users.Add(student.Object);
            var repository = new DbRepository<LearningPlusClass>(dbContext);
            var classesService = new ClassesService(null, null, repository, null);
            var createdModel = classesService.Create(model);

            var classes = classesService.GetScheduleClasses();

            classes.Count.ShouldBe(1);
        }

        [Fact]
        public void GetTeacherClassesReturnsCorrectClasses()
        {
            var teacher = new Mock<LearningPlusUser>();
            teacher.Setup(t => t.Id).Returns("teacherId");

            var model = new ClassesCreateViewModel
            {
                DayOfWeek = DaysOfWeek.Понеделник.ToString(),
                Discipline = Disciplines.Английски_език.ToString(),
                Room = Room.Стая_1.ToString(),
                TimeOfDay = "11:00 - 12:30",
                TeacherId = "teacherId",
                StudentIds = new List<string> { "studentId" }
            };

            var options = new DbContextOptionsBuilder<LearningPlusDbContext>()
                 .UseInMemoryDatabase(databaseName: "Teacher_Classes_Database") // Give a Unique name to the DB
                 .Options;
            var dbContext = new LearningPlusDbContext(options);
            dbContext.Users.Add(teacher.Object);
            dbContext.SaveChanges();
            var classRepo = new DbRepository<LearningPlusClass>(dbContext);
            var userRepo = new DbRepository<LearningPlusUser>(dbContext);

            var classesService = new ClassesService(null, null, classRepo, userRepo);

            var createdModel = classesService.Create(model);

            var teacherClasses = classesService.GetTeacherClasses("teacherId");

            teacherClasses.Count.ShouldBe(1);
        }

        //TODO: GetStudentClassesTests
    }
}
