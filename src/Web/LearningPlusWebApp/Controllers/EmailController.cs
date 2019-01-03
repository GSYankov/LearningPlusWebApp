using LearningPlus.Web.Services.EmailService;
using LearningPlus.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LearningPlus.Web.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService emailSender;

        public EmailController(IEmailService emailSender)
        {
            this.emailSender = emailSender;
        }

        [HttpPost]
        public async Task<string> SendFromHome(EmailHomeViewModel model)
        {
            string result;
           try
            {
                await emailSender.SendEmailAsync(model.Email, model.Subject, model.Message);
                result = "OK";
            }
            catch (Exception)
            {

                result = "Нещо се обърка! Моля, обадете се на посочения телефон.";
            }

            return result;
        }
    }
}
