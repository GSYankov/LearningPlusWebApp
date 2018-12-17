using AutoMapper;
using LearningPlus.Data;
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
using System.Text;

namespace LerningPlus.Web.Services.ClassesService
{
    public class ClassesService : IClassesService
    {
        private readonly IMapper mapper;
        private readonly UserManager<LearningPlusUser> userManager;
        private readonly IRepository<LearningPlusClass> classRepo;

        public ClassesService(IMapper mapper, UserManager<LearningPlusUser> userManager, IRepository<LearningPlusClass> classRepo)
        {
            this.userManager = userManager;
            this.classRepo = classRepo;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClassesCreateViewModel, LearningPlusClass>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => Enum.Parse<LearningPlus.Models.Enums.DaysOfWeek>(src.DayOfWeek)))
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => Enum.Parse<Disciplines>(src.Discipline)))
                .ForMember(dest => dest.TimeOfDay, opt => opt.MapFrom(src => Enum.Parse<TimeOfDay>($"H{src.TimeOfDay.Substring(0, 5).Remove(2, 1)}")))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src => Enum.Parse<Room>(src.Room)))
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => this.userManager.FindByIdAsync(src.TeacherId).GetAwaiter().GetResult()))
                .ForMember(dest => dest.Students, opt =>
                    opt.MapFrom(src => src.StudentIds.Select(id => this.userManager.FindByIdAsync(id).GetAwaiter().GetResult())));

                cfg.CreateMap<LearningPlusClass, ClassesScheduleViewModel>()
                .ForMember(dest => dest.StudentNamesShort, opt =>
                    opt.MapFrom(src => string.Join(", ", src.Students.Select(s => s.FirstName))))
                    .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline.ToString().Substring(0, 1)))
                    .ForMember(dest => dest.TimeOfDay, opt => opt.MapFrom(src => src.TimeOfDay.ToString().Substring(1).Insert(2, ":")));

                cfg.CreateMap<LearningPlusClass, ClassesDetailsViewModel>()
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => $"{src.Teacher.FirstName} {src.Teacher.LastName}"));
            });

            this.mapper = new Mapper(config);

        }

        public LearningPlusClass Create(ClassesCreateViewModel model)
        {

            var newClass = this.mapper.Map<LearningPlusClass>(model);

            this.classRepo.AddAsync(newClass).GetAwaiter().GetResult();
            this.classRepo.SaveChangesAsync().GetAwaiter().GetResult();

            return newClass;
        }

        public ClassesDetailsViewModel GetDetailsById(string id)
        {
            var lpClass = this.classRepo.All().Include(c=>c.Teacher).FirstOrDefault(c => c.Id.ToString() == id);
            var model = this.mapper.Map<ClassesDetailsViewModel>(lpClass);

            return model;
        }

        public ICollection<ClassesScheduleViewModel> GetScheduleClasses()
        {
            var classes = this.classRepo.All().Include(src => src.Students).Where(c => c.Active).ToList();
            var model = classes.Select(c => this.mapper.Map<ClassesScheduleViewModel>(c)).ToList();

            return model;
        }
    }
}
