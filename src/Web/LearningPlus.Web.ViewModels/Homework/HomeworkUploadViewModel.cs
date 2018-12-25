using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearningPlus.Web.ViewModels.Homework
{
    public class HomeworkUploadViewModel
    {
        public ICollection<HomeworkUploadGetViewModel> Courses { get; set; }

        [Required(ErrorMessage = "Не е избран курс!")]
        [DisplayName("Курс")]
        public string CourseId { get; set; }

        [Required(ErrorMessage = "Не е избран файл!")]
        [DisplayName("Качи файл")]
        [DataType(DataType.Upload)]
        public IFormFile Homework { get; set; }
    }
}
