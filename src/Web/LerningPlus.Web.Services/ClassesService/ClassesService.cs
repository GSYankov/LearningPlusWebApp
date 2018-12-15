using AutoMapper;
using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LearningPlus.Models.Enums;
using LearningPlus.Web.ViewModels.Classes;
using LerningPlus.Web.Services.ClassesService.Contract;
using Microsoft.AspNetCore.Identity;
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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ClassesCreateViewModel, LearningPlusClass>()
            .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => Enum.Parse<LearningPlus.Models.Enums.DaysOfWeek>(src.DayOfWeek)))
            .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => Enum.Parse<Disciplines>(src.Discipline)))
            .ForMember(dest => dest.TimeOfDay, opt => opt.MapFrom(src => Enum.Parse<TimeOfDay>($"H{src.TimeOfDay.Substring(0, 5).Remove(2, 1)}")))
            .ForMember(dest => dest.Room, opt => opt.MapFrom(src => Enum.Parse<Room>(src.Room)))
            .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => this.userManager.FindByIdAsync(src.TeacherId).GetAwaiter().GetResult()))
            .ForMember(dest => dest.Students, opt => opt
                                                .MapFrom(src => src.StudentIds.Select(id => new LearningPlusStudentsClasses() { StudentId = id }))));


            this.mapper = new Mapper(config);

        }

        public LearningPlusClass Create(ClassesCreateViewModel model)
        {

            var newClass = this.mapper.Map<LearningPlusClass>(model);

            this.classRepo.AddAsync(newClass).GetAwaiter().GetResult();
            this.classRepo.SaveChangesAsync().GetAwaiter().GetResult();

            return newClass;
        }
    }
}
