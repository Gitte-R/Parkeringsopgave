using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Gateway
    {
        private readonly HttpClient httpClient;

        public Gateway(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task SendSMS(string receiver, string licensePlate)
        {
            var user = new { receiver, licensePlate};
            //httpClient.BaseAddress = new Uri("https://localhost/SMScontroller:32770");
            await this.httpClient.PostAsync("https://localhost/SMScontroller:32770", CreateBody(user));
        }

        private static StringContent CreateBody(object user) =>
          new StringContent(
            JsonSerializer.Serialize(user),
            Encoding.UTF8,
            "application/json");
    }

}
