using AutoMapper;
using LearningPlus.Models;
using LearningPlus.Web.Areas.Teacher.ViewModels;
using LearningPlus.Web.Models;
using LearningPlus.Web.ViewModels;

namespace LearningPlus.Web.Infrastructure
{
    public class AuromapperConfiguration : Profile
    {
        public AuromapperConfiguration()
        {
            this.CreateMap<LearningPlusUser, UserDetailsViewModel>();
            this.CreateMap<LearningPlusNews, NewsLoggedInViewModel>();
        }
    }
}
