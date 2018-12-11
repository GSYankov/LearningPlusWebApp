using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlus.Web.Services.EmailSender
{
   public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage, string sender);
    }
}
