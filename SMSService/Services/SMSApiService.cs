using SMSService.Models;

namespace SMSService.Services
{
    public class SMSApiService : ISMSApiService
    {
        private readonly IConfiguration _configuration;

        public SMSApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendSMS(string receiver, string licensplate)
        {
            string url = $"https://twilioproxy.azurewebsites.net/api/SendSMS?code=biIj0VMV608PIppCMrQDNn477AqqA7-w4a7mE8kug2HvAzFuxgovmQ==";
            string key = _configuration.GetValue<string>("Key");
            SMS newSMS = new()
            {
                Receiver = receiver,
                Message = $"Din bil med nummerplade {licensplate} er registreret.",
                Key = key,
            };

            HttpClient httpClient = new HttpClient();
            await httpClient.PostAsJsonAsync(url, newSMS);
        }
    }
}
