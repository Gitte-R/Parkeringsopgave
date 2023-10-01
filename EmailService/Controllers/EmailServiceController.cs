using EmailService.Models;
using EmailService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailServiceController : Controller
    {
        private readonly IEmailApiService _emailApiService;

        public EmailServiceController(IEmailApiService emailApiService)
        {
            _emailApiService = emailApiService;

        }

        [HttpPost("SendEmail")]
        public async Task Post(string reciever, string licensePlate)
        {
            await _emailApiService.SendEmail(reciever, licensePlate);
        }
    }
}
