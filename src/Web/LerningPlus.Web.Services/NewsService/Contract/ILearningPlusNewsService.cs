﻿using System.Collections.Generic;
using System.Linq;
using LearningPlus.Models;
using LearningPlus.Web.Models;
using LearningPlus.Web.ViewModels;
using LearningPlus.Web.ViewModels.News;

namespace LerningPlus.Web.Services.NewsService.Contract
{
    public interface ILearningPlusNewsService
    {
        IQueryable<LearningPlusNews> GetById(string id);

        List<NewsLoggedInViewModel> GetAdminNews();

        List<NewsLoggedInViewModel> GetTeacherNews();

        List<NewsLoggedInViewModel> GetParentChildNews();

        LearningPlusNews EditNews(NewsCreateEditPostViewModel model, LearningPlusUser creator);

        LearningPlusNews CreateNews(NewsCreateEditPostViewModel model, LearningPlusUser creator);

        LearningPlusNews FakeDelete(string id);

        ICollection<NewsArchiveViewModel> GetArchivedNews();
    }
}
