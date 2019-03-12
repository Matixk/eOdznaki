using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace eOdznaki.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                var SETTINGS_STRING = "EmailSettings:";
                var credentials = new NetworkCredential
                {
                    UserName = configuration[$"{SETTINGS_STRING}Email"],
                    Password = configuration[$"{SETTINGS_STRING}Password"]
                };

                client.Credentials = credentials;
                client.Host = configuration[$"{SETTINGS_STRING}Domain"];
                client.Port = int.Parse(configuration[$"{SETTINGS_STRING}Port"]);
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress(credentials.UserName);
                    emailMessage.Subject = subject;
                    emailMessage.Body = message;
                    client.Send(emailMessage);
                }
            }

            await Task.CompletedTask;
        }
    }
}