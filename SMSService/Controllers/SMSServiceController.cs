using Microsoft.AspNetCore.Mvc;
using SMSService.Services;

namespace SMSService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SMSServiceController : ControllerBase
    {
        private readonly ISMSApiService _smsApiService;

        public SMSServiceController(ISMSApiService smsApiService)
        {
            _smsApiService = smsApiService;
        }

        [HttpPost("SMS")]
        public async Task SendSMS(string receiver)
        {
            await _smsApiService.SendSMS(receiver);
        }
    }
}