using ParkingService.Models;
using ParkingService.Events;

namespace ParkingService.Services
{
    public class ParkingStore : IParkingStore
    {
        private readonly IEventStore eventStore;

        public ParkingStore(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        private static readonly Dictionary<string, Parking> ParkingsDatabase = new Dictionary<string, Parking>();
        public Parking Get(string licenseplate) => ParkingsDatabase.ContainsKey(licenseplate) ? ParkingsDatabase[licenseplate] : new Parking(licenseplate);

        public void Save(string licenseplate, string parkinglot, string? phonenumber, string? email) 
        {
            Parking NewParking = new Parking(licenseplate)
            {
                Parkinglot = parkinglot,
                Time = DateTime.Now,
                Phonenumber = phonenumber,
                Email = email
            };

            this.eventStore.RaiseEvent("ParkingStarted", NewParking);
            ParkingsDatabase[NewParking.Licenseplate] = NewParking;
        }


        public void Remove(string licenseplate)
        {
            if (ParkingsDatabase.ContainsKey(licenseplate))
            {
                this.eventStore.RaiseEvent("ParkingEnded", ParkingsDatabase[licenseplate]);
                ParkingsDatabase.Remove(licenseplate);
            }
        }

    }
}