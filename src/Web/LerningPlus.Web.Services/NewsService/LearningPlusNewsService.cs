﻿using AutoMapper;
using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LearningPlus.Web.ViewModels;
using LerningPlus.Web.Services.NewsService.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using LearningPlus.Web.Models;
using LearningPlus.Web.ViewModels.News;
using LearningPlus.Web.Models.Enums;

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

        public IQueryable<LearningPlusNews> GetById(string id) => this.newsRepo.All()
            .Include(n => n.Creator)
            .Include(n => n.TargetRoles)
            .Where(n => n.Id.ToString() == id);

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

        public void EditNews(NewsCreateEditPostViewModel model, LearningPlusUser creator)
        {
            var news = this.GetById(model.Id).FirstOrDefault();
            List<LearningPlusNewsTargetRole> targetRoles = AddTargetRolesToANews(model);

            news.Message = model.Message;
            news.Creator = creator;
            news.ExpiresOn = model.ExpiresOn;
            news.TargetRoles = targetRoles;

            this.newsRepo.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void CreateNews(NewsCreateEditPostViewModel model, LearningPlusUser creator)
        {
            List<LearningPlusNewsTargetRole> targetRoles = AddTargetRolesToANews(model);

            var news = new LearningPlusNews
            {
                Message = model.Message,
                Creator = creator,
                ExpiresOn = model.ExpiresOn,
                TargetRoles = targetRoles,
            };

            this.newsRepo.AddAsync(news).GetAwaiter().GetResult();
            this.newsRepo.SaveChangesAsync().GetAwaiter().GetResult();
        }

        private static List<LearningPlusNewsTargetRole> AddTargetRolesToANews(NewsCreateEditPostViewModel model)
        {
            var targetRoles = new List<LearningPlusNewsTargetRole>();
            if (model.roleAdmin)
            {
                var targetRole = new LearningPlusNewsTargetRole
                {
                    TargetRole = UserRoles.Admin
                };
                targetRoles.Add(targetRole);
            }

            if (model.roleTeacher)
            {
                var targetRole = new LearningPlusNewsTargetRole
                {
                    TargetRole = UserRoles.Teacher
                };
                targetRoles.Add(targetRole);
            }

            if (model.roleChildrenParents)
            {
                var targetRole = new LearningPlusNewsTargetRole
                {
                    TargetRole = UserRoles.Parent
                };
                targetRoles.Add(targetRole);

                targetRole = new LearningPlusNewsTargetRole
                {
                    TargetRole = UserRoles.Child
                };
                targetRoles.Add(targetRole);
            }

            return targetRoles;
        }
    }
}
