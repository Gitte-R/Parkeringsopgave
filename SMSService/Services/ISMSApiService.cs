namespace SMSService.Services
{
    public interface ISMSApiService
    {
        public Task SendSMS(string reciever);
    }
}
