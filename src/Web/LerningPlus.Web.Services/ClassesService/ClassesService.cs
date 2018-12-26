using AutoMapper;
using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LearningPlus.Models.Enums;
using LearningPlus.Web.ViewModels.Classes;
using LerningPlus.Web.Services.ClassesService.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace LerningPlus.Web.Services.ClassesService
{
    public class ClassesService : IClassesService
    {
        private readonly IMapper mapper;
        private readonly UserManager<LearningPlusUser> userManager;
        private readonly IRepository<LearningPlusClass> classesRepo;
        private readonly IRepository<LearningPlusUser> usersRepo;

        public ClassesService(IMapper mapper,
            UserManager<LearningPlusUser> userManager,
            IRepository<LearningPlusClass> classRepo,
            IRepository<LearningPlusUser> usersRepo)
        {
            this.userManager = userManager;
            this.classesRepo = classRepo;
            this.usersRepo = usersRepo;
            MapperConfiguration config = MapperConfiguration();

            this.mapper = new Mapper(config);

        }

        private MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClassesCreateViewModel, LearningPlusClass>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => Enum.Parse<DaysOfWeek>(src.DayOfWeek)))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => Enum.Parse<Disciplines>(src.Discipline)))
                .ForMember(dest => dest.TimeOfDay, opt => opt.MapFrom(src => Enum.Parse<TimeOfDay>($"H{src.TimeOfDay.Substring(0, 5).Remove(2, 1)}")))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src => Enum.Parse<Room>(src.Room)))
                // .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => this.userManager.FindByIdAsync(src.TeacherId).GetAwaiter().GetResult()))
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => this.usersRepo.All().SingleOrDefault(u => u.Id == src.TeacherId)))
                .ForMember(dest => dest.StudentsEnrolled, opt =>
                    opt.MapFrom(src => src.StudentIds.Select(id => new LearningPlusClassesStudents { StudentId = id })));

                cfg.CreateMap<LearningPlusClass, ClassesScheduleViewModel>()
                .ForMember(dest => dest.StudentNamesShort, opt =>
                    opt.MapFrom(src => string.Join(", ", src.StudentsEnrolled.Select(s =>
                   this.userManager.FindByIdAsync(s.StudentId).GetAwaiter().GetResult().FirstName))))
                    .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline.ToString().Substring(0, 1)))
                    .ForMember(dest => dest.TimeOfDay, opt => opt.MapFrom(src => src.TimeOfDay.ToString().Substring(1).Insert(2, ":")));

                cfg.CreateMap<LearningPlusClass, ClassesDetailsViewModel>()
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => $"{src.Teacher.FirstName} {src.Teacher.LastName}"))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline.ToString().Replace('_', ' ')))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room.ToString().Replace('_', ' ')))
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.StudentsEnrolled.Select(s =>
                  this.userManager.FindByIdAsync(s.StudentId).GetAwaiter().GetResult())))
                .ForMember(dest => dest.TimeOfDay, opt => opt.MapFrom(src => src.TimeOfDay.ToString().Substring(1).Insert(2, ":") + " ч."));
            });
        }

        public LearningPlusClass Create(ClassesCreateViewModel model)
        {

            var newClass = this.mapper.Map<LearningPlusClass>(model);

            this.classesRepo.AddAsync(newClass).GetAwaiter().GetResult();
            this.classesRepo.SaveChangesAsync().GetAwaiter().GetResult();

            return newClass;
        }

        public LearningPlusClass DeleteById(string id)
        {
            var lpclass = this.classesRepo.All().SingleOrDefault(c => c.Id.ToString() == id);
            lpclass.Active = false;
            this.classesRepo.SaveChangesAsync().GetAwaiter().GetResult();

            return lpclass;
        }

        public ClassesDetailsViewModel GetDetailsById(string id)
        {
            var lpClass = this.classesRepo.All().Include(c => c.Teacher).Include(c => c.StudentsEnrolled).FirstOrDefault(c => c.Id.ToString() == id);
            var model = this.mapper.Map<ClassesDetailsViewModel>(lpClass);

            return model;
        }

        public ICollection<ClassesScheduleViewModel> GetScheduleClasses()
        {
            var classes = this.classesRepo.All().Include(s => s.StudentsEnrolled).Where(c => c.Active).ToList();
            var model = classes.Select(c => this.mapper.Map<ClassesScheduleViewModel>(c)).ToList();

            return model;
        }

        public ICollection<LearningPlusClass> GetStudentClasses(ClaimsPrincipal user)
        {
            var users = usersRepo.All().Include(u => u.ClassesEnrolled).ToList();
            var userClasses = userManager.GetUserAsync(user).GetAwaiter().GetResult().ClassesEnrolled.ToList();
            var classes = userClasses.Select(c => this.classesRepo.All().SingleOrDefault(x => x.Id == c.ClassId)).ToList();

            return classes;
        }

        public ICollection<LearningPlusClass> GetTeacherClasses(string userId)
        {
            var classes = classesRepo.All().Where(c => c.Teacher.Id == userId).ToList();

            return classes;
        }
    }
}

