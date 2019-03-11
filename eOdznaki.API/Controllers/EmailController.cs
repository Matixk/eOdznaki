using eOdznaki.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eOdznaki.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender emailSender;
        public EmailController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailAsync([FromBody]string email, string subject, string message)
        {
            await emailSender.SendEmailAsync(email, subject, message);
            return Ok();
        }
    }
}
