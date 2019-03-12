using System.Threading.Tasks;

namespace eOdznaki.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}