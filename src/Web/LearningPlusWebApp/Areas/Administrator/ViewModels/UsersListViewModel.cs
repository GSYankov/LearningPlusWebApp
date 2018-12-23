using System.ComponentModel.DataAnnotations;

namespace LearningPlus.Web.Areas.Administrator.ViewModels
{
    public class UsersListViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Име")]
        public string UserName { get; set; }

        [Display(Name = "Имейл")]
        public string Email { get; set; }

        [Display(Name = "Настояща роля")]
        public string Role { get; set; }
    }
}
