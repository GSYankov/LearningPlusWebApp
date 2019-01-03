using Microsoft.Extensions.Configuration;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LearningPlus.Web.Services.EmailService
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public SmtpEmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<MailMessage> SendEmailAsync(string email, string subject, string htmlMessage)
        {

            return await Task.Run(() => this.SendSmtpClient(email, subject, htmlMessage));
        }

        public async Task<MailMessage> SendEmailAsync(string email, string subject, string htmlMessage, string sender)
        {

            return await Task.Run(() => this.SendSmtpClient(email, subject, htmlMessage));
        }

        private MailMessage SendSmtpClient(string email, string subject, string htmlMessage, string sender = null)
        {
            if (sender == null)
            {
                sender = this.configuration["Email:Sender"];
            }

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(email));
            msg.From = new MailAddress(sender);
            msg.Subject = subject;
            msg.Body = "<h1>От " + sender + "</h1><br/><p>" + htmlMessage + "<p>";
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient
            {
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(
                this.configuration["Email:Sender"],
                this.configuration["Email:Password"]),

                Port = 587, // Use Port 25 if 587 is blocked.
                Host = this.configuration["Email:Server"],
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };
            client.Send(msg);

            return msg;
        }
    }
}
