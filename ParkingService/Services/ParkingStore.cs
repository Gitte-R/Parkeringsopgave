using System.Linq;
using EventService.Serivces;
using ParkingService.Models;
using SMSService.Services;
using EmailService.Services;

namespace ParkingService.Services
{
    public class ParkingStore : IParkingStore
    {
        private readonly IEventStore _eventStore;
        private readonly ISMSApiService _smsApiService;
        private readonly IEmailApiService _emailApiService;

        public ParkingStore(IEventStore eventstore, ISMSApiService smsApiService, IEmailApiService emailApiService)
        {
            _eventStore = eventstore;
            _smsApiService = smsApiService;
            _emailApiService = emailApiService;
        }

        private static readonly Dictionary<string, Parking> ParkingsDatabase = new Dictionary<string, Parking>();
        public Parking Get(string licenseplate) => ParkingsDatabase.ContainsKey(licenseplate) ? ParkingsDatabase[licenseplate] : new Parking(licenseplate);

        public void Save(string licenseplate, string parkinglot, string? phonenumber, string? email) 
        {
            Parking NewParking = new(licenseplate);
            {
                NewParking.Parkinglot = parkinglot;
                NewParking.Time = DateTime.Now;
                NewParking.Phonenumber = phonenumber;
                NewParking.Email = email;
            };

            ParkingsDatabase[NewParking.Licenseplate] = NewParking;

            if (phonenumber != null)
            {
                _smsApiService.SendSMS((string)phonenumber, licenseplate);
            }
            if (email != null)
            {
                _emailApiService.SendEmail(email, licenseplate);
            }

            _eventStore.Raise(NewParking);
        }


        public void Remove(string licenseplate)
        {
            if (ParkingsDatabase.ContainsKey(licenseplate))
            {
                ParkingsDatabase.Remove(licenseplate);
            }
        }

    }
}