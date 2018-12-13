using AutoMapper;
using LearningPlus.Models;
using LearningPlus.Web.Areas.Teacher.ViewModels;
using LearningPlus.Web.Models;
using LearningPlus.Web.ViewModels;
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
            opt => opt.MapFrom(src => src.TargetRoles.Select(tr => tr.TargetRole.ToString() )));


        }
    }
}
