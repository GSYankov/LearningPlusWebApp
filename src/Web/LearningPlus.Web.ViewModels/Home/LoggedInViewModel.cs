using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlus.Web.ViewModels.Home
{
    public class LoggedInViewModel
    {
        public IEnumerable<NewsLoggedInViewModel> NewsViewModel {get; set;}

        public IEnumerable<ClassesLoggedInViewModel> ClassesViewModel {get; set; }

        public IEnumerable<HomeworkLoggedInViewModel> HomeworksViewModel { get; set; }
    }
}
