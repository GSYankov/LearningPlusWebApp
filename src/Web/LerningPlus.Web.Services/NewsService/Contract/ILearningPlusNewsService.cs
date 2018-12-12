using System.Collections.Generic;
using LearningPlus.Models;
using LearningPlus.Web.ViewModels;

namespace LerningPlus.Web.Services.NewsService.Contract
{
    public  interface ILearningPlusNewsService
    {
        List<NewsLoggedInViewModel> GetAdminNews();

        List<NewsLoggedInViewModel> GetTeacherNews();

        List<NewsLoggedInViewModel> GetParentChildNews();
    }
}
