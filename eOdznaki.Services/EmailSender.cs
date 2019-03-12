using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using eOdznaki.Interfaces;
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
                const string settingsString = "EmailSettings:";
                var credentials = new NetworkCredential
                {
                    UserName = configuration[$"{settingsString}Email"],
                    Password = configuration[$"{settingsString}Password"]
                };

                client.Credentials = credentials;
                client.Host = configuration[$"{settingsString}Domain"];
                client.Port = int.Parse(configuration[$"{settingsString}Port"]);
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