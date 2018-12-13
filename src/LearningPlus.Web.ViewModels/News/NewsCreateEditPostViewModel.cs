using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlus.Web.ViewModels.News
{
   public class NewsCreateEditPostViewModel
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public bool roleAdmin { get; set; }

        public bool roleTeacher { get; set; }

        public bool roleChildrenParents { get; set; }

        public DateTime ExpiresOn { get; set; }
    }
}
