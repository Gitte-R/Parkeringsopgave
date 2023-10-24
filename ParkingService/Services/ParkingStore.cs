using ParkingService.Models;
using ParkingService.Events;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Hosting;

namespace ParkingService.Services
{
    public class ParkingStore : IParkingStore
    {
        private readonly IEventStore eventStore;
        private readonly HttpClient httpClient;

        public ParkingStore(IEventStore eventStore, HttpClient httpClient)
        {
            this.eventStore = eventStore;
            this.httpClient = httpClient;
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