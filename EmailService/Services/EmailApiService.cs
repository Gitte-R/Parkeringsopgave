using EmailService.Models;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using EventService.Serivces;

namespace EmailService.Services
{
    public class EmailApiService : IEmailApiService
    {
        private readonly IEventStore _eventStore;

        public EmailApiService(IEventStore eventstore)
        {
            _eventStore = eventstore;

        }
        public async Task SendEmail(string receiver, string licensePlate)
        {
            string url = $"https://twilioproxy.azurewebsites.net/api/SendEmail?code=qMTJzZtnKGD4c0LgyYHyepoT7VdFOir1Wig9yrU6LeQLAzFuCJeiWw==";

            Email newEmail = new Email()
            {
                Receiver = receiver,
                Subject = $"Registrering af {licensePlate}",
                Message = $"Din bil med numemrplade {licensePlate} er nu reigstreret og parkeringen er startet."
            };

            _eventStore.Raise(newEmail);

            HttpClient httpClient = new HttpClient();
            await httpClient.PostAsJsonAsync(url, newEmail);
        }
    }
}
