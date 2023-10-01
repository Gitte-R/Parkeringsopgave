using EventService.Serivces;
using Microsoft.Extensions.Configuration;
using SMSService.Models;
using System.Net.Http;

namespace SMSService.Services
{
    public class SMSApiService : ISMSApiService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventStore _eventstore;

        public SMSApiService(IConfiguration configuration, IEventStore eventstore)
        {
            _configuration = configuration;
            _eventstore = eventstore;
        }
        public async Task SendSMS(string receiver, string licensePlate)
        {
            string url = $"https://twilioproxy.azurewebsites.net/api/SendSMS?code=biIj0VMV608PIppCMrQDNn477AqqA7-w4a7mE8kug2HvAzFuxgovmQ==";
            string key = _configuration.GetValue<string>("Key");
            SMS newSMS = new()
            {
                Receiver = receiver,
                Message = $"Din bil med nummerplade {licensePlate} er registreret.",
                Key = key,
            };

            _eventstore.Raise(newSMS);

            HttpClient httpClient = new HttpClient();
            await httpClient.PostAsJsonAsync(url, newSMS);
        }
    }
}
