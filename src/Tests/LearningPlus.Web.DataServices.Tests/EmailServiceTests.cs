using LearningPlus.Web.Services.EmailService;
using System.Threading.Tasks;
using Xunit;

namespace LearningPlus.Web.DataServices.Tests
{
    public class EmailServiceTests
    {
        [Fact]
        public async Task SendEmailShouldReturnEmailMessage()
        {
            var configuration = TestsPrerequisites.GetConfiguration();
            var emailService = new SmtpEmailService(configuration);
            var receiver = "spookyfox@abv.bg";
            var subject = "Xunit test";
            var message = "Xunit test message";
            var sender = "LearningPlus Tests";

            var emailMessage = await emailService.SendEmailAsync(receiver, subject, message, sender);

            Assert.Equal(message, emailMessage.Body);
        }
    }
}
