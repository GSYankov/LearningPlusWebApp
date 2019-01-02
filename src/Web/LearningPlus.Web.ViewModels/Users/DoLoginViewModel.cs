using System.ComponentModel.DataAnnotations;

namespace LearningPlus.Web.ViewModels
{
    public class DoLoginViewModel
    {
        [Required(ErrorMessage = "Потребителското име е задължително!")]
        [RegularExpression(@"[A-z 0-9]+", ErrorMessage = "Потребитлското име трябва да е на латиница!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Паролата е задължителна!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
