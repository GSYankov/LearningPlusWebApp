using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LearningPlus.Web.ViewModels.News
{
    public class NewsArchiveViewModel
    {
        [DisplayName("#")]
        public string Id { get; set; }

        [DisplayName("Създадена на")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Създател")]
        public string Creator { get; set; }

        [DisplayName("Текст")]
        public string Message { get; set; }

        [DisplayName("Целеви групи")]
        public string TargetRoles { get; set; }

        [DisplayName("Изтекла на")]
        public DateTime ExpiresOn { get; set; }
    }
}
