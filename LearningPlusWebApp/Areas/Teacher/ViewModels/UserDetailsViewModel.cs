using System.ComponentModel.DataAnnotations;

namespace LearningPlusWebApp.Areas.Teacher.ViewModels
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }

        [RegularExpression("[A-Za-z0-9]+")]
        [Display(Name = "Потребителско име")]
        [Required(ErrorMessage = "Полето 'Потребителско име' е задължително")]
        public string UserName { get; set; }

        [Display(Name = "Име")]
        [Required(ErrorMessage = "Полето 'Име' е задължително")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Полето 'Фамилия' е задължително")]
        public string LastName { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Полето 'Телефон' е задължително")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; }
    }
}
