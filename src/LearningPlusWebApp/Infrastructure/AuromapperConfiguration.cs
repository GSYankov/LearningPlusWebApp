using AutoMapper;
using LearningPlus.Web.Areas.Teacher.ViewModels;
using LearningPlus.Web.Models;

namespace LearningPlus.Web.Infrastructure
{
    public class AuromapperConfiguration : Profile
    {
        public AuromapperConfiguration()
        {
            this.CreateMap<LearningPlusUser, UserDetailsViewModel>();
        }
    }
}
