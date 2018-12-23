using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LearningPlus.Web.ViewModels.News
{
  public  class NewsEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [DisplayName("Текст:")]
        public string Message { get; set; }

        [DisplayName("Целеви групи:")]
        public ICollection<string> TargetRoles { get; set; }

        [Required]
        [DisplayName("Изтича на:")]
        public DateTime ExpiresOn { get; set; }
    }
}
