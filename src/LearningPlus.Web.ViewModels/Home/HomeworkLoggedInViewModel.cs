using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlus.Web.ViewModels.Home
{
   public class HomeworkLoggedInViewModel
    {
        public Guid Id { get; set; }

        public DateTime UploadedOn { get; set; } = DateTime.UtcNow;

        public string BlobLink { get; set; }

        public string Discipline { get; set; }

        public string Student { get; set; }
    }
}
