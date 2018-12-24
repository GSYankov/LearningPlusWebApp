using System.Net.Mail;
using System.Threading.Tasks;

namespace LearningPlus.Web.Services.EmailService
{
    public interface IEmailService
    {
        Task<MailMessage> SendEmailAsync(string email, string subject, string htmlMessage, string sender);

        Task<MailMessage> SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
