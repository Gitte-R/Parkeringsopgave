using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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

        public async Task SendSMS(string receiver, string licensplate)
        {
            //var user = new { receiver, licensplate};
            //httpClient.BaseAddress = new Uri("https://localhost/SMScontroller:32770");
            //await this.httpClient.PostAsync($"https://localhost/SMScontroller:32770?reciever={receiver}&licensplate={licensplate}", CreateBody(user));

            SMS newsms = new(receiver, licensplate);
            await httpClient.PostAsJsonAsync($"https://localhost:32770/SMScontroller", newsms);
        }

        //private static StringContent CreateBody(object user) =>
        //  new StringContent(
        //    JsonSerializer.Serialize(user),
        //    Encoding.UTF8,
        //    "application/json");
    }
    public record SMS(string receiver, string licensplate);

}
