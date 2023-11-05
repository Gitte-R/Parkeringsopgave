using EmailService.Models;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmailService.Services
{
    public class EmailApiService : IEmailApiService
    {
        public async Task SendEmail(string receiver, string licensplate)
        {
            string url = $"https://twilioproxy.azurewebsites.net/api/SendEmail?code=qMTJzZtnKGD4c0LgyYHyepoT7VdFOir1Wig9yrU6LeQLAzFuCJeiWw==";

            Email newEmail = new Email()
            {
                Receiver = receiver,
                Subject = $"Registrering af {licensplate}",
                Message = $"Din bil med nummerplade {licensplate} er nu reigstreret og parkeringen er startet."
            };

            HttpClient httpClient = new HttpClient();
            await httpClient.PostAsJsonAsync(url, newEmail);
        }
    }
}
