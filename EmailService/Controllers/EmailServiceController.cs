using EmailService.Models;
using EmailService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    [ApiController]
    [Route("/Emailcontroller/")]
    public class EmailServiceController : Controller
    {
        private readonly IEmailApiService _emailApiService;

        public EmailServiceController(IEmailApiService emailApiService)
        {
            _emailApiService = emailApiService;

        }

        [HttpPost("")]
        public async Task Post(string receiver, string licensPlate)
        {
            await _emailApiService.SendEmail(receiver, licensPlate);
        }
    }
}
