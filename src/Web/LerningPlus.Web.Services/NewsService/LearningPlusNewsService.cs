using AutoMapper;
using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LearningPlus.Web.ViewModels;
using LerningPlus.Web.Services.NewsService.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LerningPlus.Web.Services.NewsService
{
    public class LearningPlusNewsService : ILearningPlusNewsService
    {
        private const int newsToShow = 5;

        private readonly IMapper mapper;
        private readonly IRepository<LearningPlusNews> newsRepo;

        public LearningPlusNewsService(IMapper mapper, IRepository<LearningPlusNews> repository)
        {
            this.mapper = mapper;
            this.newsRepo = repository;
        }
        public List<NewsLoggedInViewModel> GetAdminNews()
        {
            return this.newsRepo.All()
                          .Where(n => n.ExpiresOn > DateTime.UtcNow)
                          .Take(newsToShow)
                          .Select(n => this.mapper.Map<NewsLoggedInViewModel>(n))
                          .ToList();
        }

        public List<NewsLoggedInViewModel> GetTeacherNews()
        {
            return this.newsRepo.All()
                          .Where(n => n.ExpiresOn > DateTime.UtcNow
                                  && n.TargetRoles.Any(tr => tr.TargetRole.ToString() != "Admin"))
                          .Take(newsToShow)
                          .Select(n => this.mapper.Map<NewsLoggedInViewModel>(n))
                          .ToList();
        }

        public List<NewsLoggedInViewModel> GetParentChildNews()
        {
            return this.newsRepo.All()
                          .Where(n => n.ExpiresOn > DateTime.UtcNow
                                  && n.TargetRoles.Any(tr => tr.TargetRole.ToString() != "Admin"
                                                          && tr.TargetRole.ToString() != "Teacher"))
                          .Take(newsToShow)
                          .Select(n => this.mapper.Map<NewsLoggedInViewModel>(n))
                          .ToList();
        }
    }
}
