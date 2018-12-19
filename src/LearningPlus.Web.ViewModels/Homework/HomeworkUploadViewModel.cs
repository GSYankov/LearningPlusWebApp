using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningPlus.Web.ViewModels.Homework
{
    public class HomeworkUploadViewModel
    {
        [DisplayName("Курс")]
        public string CourseId { get; set; }

        [DisplayName("Качи файл")]
        [DataType(DataType.Upload)]
        public IFormFile Homework { get; set; }
    }
}
