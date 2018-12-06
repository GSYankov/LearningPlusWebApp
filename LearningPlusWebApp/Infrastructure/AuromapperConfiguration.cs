using AutoMapper;
using LearningPlusWebApp.Areas.Teacher.ViewModels;
using LearningPlusWebApp.Models;

namespace LearningPlusWebApp.Infrastructure
{
    public class AuromapperConfiguration : Profile
    {
        public AuromapperConfiguration()
        {
            this.CreateMap<LearningPlusUser, UserDetailsViewModel>();
        }
    }
}
