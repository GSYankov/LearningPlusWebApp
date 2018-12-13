using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlus.Web.ViewModels.News
{
   public class NewsCreateEditPostViewModel
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public bool RoleAdmin { get; set; }

        public bool RoleTeacher { get; set; }

        public bool RoleChildrenParents { get; set; }

        public DateTime ExpiresOn { get; set; }
    }
}
