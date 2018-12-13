using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningPlus.Web.ViewModels.News
{
    public class NewsDetailsViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Създадена на:")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Публикувал:")]
        public string Creator { get; set; }

        [Display(Name = "Съдържание:")]
        public string Message { get; set; }
    }
}
