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
            SMS newsms = new(receiver, licensplate);
            await httpClient.PostAsJsonAsync($"https://localhost:32770/SMScontroller", newsms);
        }
    }
    public record SMS(string receiver, string licensplate);

}
