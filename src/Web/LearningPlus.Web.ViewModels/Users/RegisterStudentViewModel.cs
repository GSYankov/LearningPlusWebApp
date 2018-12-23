using System.ComponentModel.DataAnnotations;

namespace LearningPlus.Web.ViewModels.Users
{
    public class RegisterStudentViewModel
    {
        [RegularExpression("[A-Za-z0-9]+")]
        [Display(Name = "Потребителско име на латиница")]
        [Required(ErrorMessage = "Полето 'Потребителско име' е задължително")]
        public string ParentUserName { get; set; }

        [Display(Name = "Име")]
        [Required(ErrorMessage = "Полето 'Име' е задължително")]
        public string ParentFirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Полето 'Фамилия' е задължително")]
        public string ParentLastName { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Полето 'Телефон' е задължително")]
        public string ParentPhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Имейл")]
        public string ParentEmail { get; set; }

        [Display(Name = "Парола")]
        [MinLength(3, ErrorMessage = "Паролата трябва да бъде поне три символа!")]
        [Required(ErrorMessage = "Полето 'Парола' е задължително")]
        public string ParentPassword { get; set; }

        [Display(Name = "Потвърди парола")]
        [MinLength(3, ErrorMessage = "Паролата трябва да бъде поне три символа!")]
        [Required(ErrorMessage = "Полето 'Потвърди парола' е задължително")]
        [Compare("ParentPassword", ErrorMessage = "Паролите на родителя не съвпадат")]
        public string ParentConfirmPassword { get; set; }

        [RegularExpression("[A-Za-z0-9]+")]
        [Display(Name = "Потребителско име на латиница")]
        [Required(ErrorMessage = "Полето 'Потребителско име' е задължително")]
        public string ChildUserName { get; set; }

        [Display(Name = "Име")]
        [Required(ErrorMessage = "Полето 'Име' е задължително")]
        public string ChildFirstName { get; set; }

        [Required(ErrorMessage = "Полето 'Фамилия' е задължително")]
        [Display(Name = "Фамилия")]
        public string ChildLastName { get; set; }

        [Display(Name = "Телефон")]
        public string ChildPhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Имейл")]
        public string ChildEmail { get; set; }

        [Display(Name = "Парола")]
        [MinLength(3, ErrorMessage = "Паролата трябва да бъде поне три символа!")]
        [Required(ErrorMessage = "Полето 'Парола' е задължително")]
        public string ChaildPassword { get; set; }

        [Display(Name = "Потвърди парола")]
        [MinLength(3, ErrorMessage = "Паролата трябва да бъде поне три символа!")]
        [Required(ErrorMessage = "Полето 'Потвърди парола' е задължително")]
        [Compare("ChaildPassword", ErrorMessage = "Паролите на ученика не съвпадат")]
        public string ChaildConfirmPassword { get; set; }
    }
}
