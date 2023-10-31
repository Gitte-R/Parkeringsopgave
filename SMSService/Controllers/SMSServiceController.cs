using Microsoft.AspNetCore.Mvc;
using SMSService.Services;


namespace SMSService.Controllers
{
    [ApiController]
    [Route("/SMScontroller/")]
    public class SMSServiceController : ControllerBase
    {
        private readonly ISMSApiService _smsApiService;
        public record SMS(string receiver, string licensplate);

        public SMSServiceController(ISMSApiService smsApiService)
        {
            _smsApiService = smsApiService;
        }

        [HttpPost("")]
        public async Task SendSMS(SMS sms)
        {
            await _smsApiService.SendSMS(sms.receiver, sms.licensplate);
        }
    }
}