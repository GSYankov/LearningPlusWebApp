using AutoMapper;
using LearningPlus.Models;
using LearningPlus.Web.Areas.Teacher.ViewModels;
using LearningPlus.Web.Models;
using LearningPlus.Web.ViewModels;
using LearningPlus.Web.ViewModels.Home;
using LearningPlus.Web.ViewModels.News;
using System.Collections.Generic;
using System.Linq;

namespace LearningPlus.Web.Infrastructure
{
    public class AuromapperConfiguration : Profile
    {
        public AuromapperConfiguration()
        {
            this.CreateMap<LearningPlusUser, UserDetailsViewModel>();

            this.CreateMap<LearningPlusNews, NewsLoggedInViewModel>();

            this.CreateMap<LearningPlusNews, NewsDetailsViewModel>()
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator.FirstName));

            this.CreateMap<LearningPlusNews, NewsEditViewModel>()
            .ForMember(dest => dest.TargetRoles,
            opt => opt.MapFrom(src => src.TargetRoles.Select(tr => tr.TargetRole.ToString())));

            this.CreateMap<LearningPlusClass, ClassesLoggedInViewModel>()
            .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline.ToString().Replace('_', ' ')))
            .ForMember(dest => dest.TimeOfDay, opt => opt.MapFrom(src => src.TimeOfDay.ToString().Substring(1).Insert(2, ":") + " ч."));

            this.CreateMap<LearningPlusHomeWork, HomeworkLoggedInViewModel>()
            .ForMember(dest => dest.Student, opt => opt.MapFrom(src => $"{src.Student.FirstName} {src.Student.LastName}"))
            .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Course.Discipline.ToString().Replace('_', ' ')));
        }
    }
}
