using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LearningPlusWebApp.Services.EmailSender
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public SmtpEmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {

            await Task.Run(() => this.SendSmtpClient(email, subject, htmlMessage));
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage, string sender)
        {

            await Task.Run(() => this.SendSmtpClient(email, subject, htmlMessage));
        }

        private void SendSmtpClient(string email, string subject, string htmlMessage, string sender = null)
        {
            if (sender == null)
            {
                sender = this.configuration["Email:Sender"];
            }

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(email));
            msg.From = new MailAddress(sender);
            msg.Subject = subject;
            msg.Body = htmlMessage;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(
                this.configuration["Email:Sender"],
                this.configuration["Email:Password"]);

            client.Port = 587; // Use Port 25 if 587 is blocked.
            client.Host = this.configuration["Email:Server"];
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Send(msg);

            return;
        }
    }
}
