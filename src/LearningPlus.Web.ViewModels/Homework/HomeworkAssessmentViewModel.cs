using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LearningPlus.Web.ViewModels.Homework
{
   public class HomeworkAssessmentViewModel
    {
        public string Id { get; set; }

        [DisplayName("Предадена на")]
        public DateTime UploadedOn { get; set; }

        [DisplayName("Изтегли")]
        public string BlobLink { get; set; }

        [DisplayName("Ученик")]
        public string Student { get; set; }

        [DisplayName("Курс")]
        public string Course { get; set; }

        [DisplayName("Рецензия")]
        public string Resolutions { get; set; }
    }
}
