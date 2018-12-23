using LearningPlus.Web.ViewModels.Home;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LearningPlus.Web.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailSender emailSender;

        public EmailController(IEmailSender emailSender)
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

                result = "Something went wrong. Please call our mobile.";
            }


            return result;
        }
    }
}
