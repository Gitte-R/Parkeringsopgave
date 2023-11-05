using EmailService.Models;
using EmailService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    [ApiController]
    [Route("/Emailcontroller/")]
    public class EmailServiceController : ControllerBase
    {
        private readonly IEmailApiService _emailApiService;
        public record Email(string receiver, string licensplate);

        public EmailServiceController(IEmailApiService emailApiService)
        {
            _emailApiService = emailApiService;

        }

        [HttpPost("")]
        public async Task Post(Email email)
        {
            await _emailApiService.SendEmail(email.receiver, email.licensplate);
        }
    }
}
