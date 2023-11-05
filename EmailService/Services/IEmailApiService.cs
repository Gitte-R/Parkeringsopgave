using EmailService.Models;

namespace EmailService.Services
{
    public interface IEmailApiService
    {
        public Task SendEmail(string receiver, string licensplate);
    }
}
