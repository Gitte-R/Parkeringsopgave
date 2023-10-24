using Microsoft.AspNetCore.Mvc;
using SMSService.Services;

namespace SMSService.Controllers
{
    [ApiController]
    [Route("/SMScontroller/")]
    public class SMSServiceController : ControllerBase
    {
        private readonly ISMSApiService _smsApiService;

        public SMSServiceController(ISMSApiService smsApiService)
        {
            _smsApiService = smsApiService;
        }

        [HttpPost("")]
        public async Task SendSMS(string receiver, string licensePlate)
        {
            await _smsApiService.SendSMS(receiver, licensePlate);
        }
    }
}