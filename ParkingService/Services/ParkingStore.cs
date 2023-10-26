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
        public Parking Get(string licensplate) => ParkingsDatabase.ContainsKey(licensplate) ? ParkingsDatabase[licensplate] : new Parking(licensplate);

        public void Save(string licensplate, string parkinglot, string? phonenumber, string? email) 
        {
            Parking NewParking = new Parking(licensplate)
            {
                Parkinglot = parkinglot,
                Time = DateTime.Now,
                Phonenumber = phonenumber,
                Email = email
            };

            this.eventStore.RaiseEvent("ParkingStarted", NewParking);
            ParkingsDatabase[NewParking.Licensplate] = NewParking;
        }

        public void Remove(string licensplate)
        {
            if (ParkingsDatabase.ContainsKey(licensplate))
            {
                this.eventStore.RaiseEvent("ParkingEnded", ParkingsDatabase[licensplate]);
                ParkingsDatabase.Remove(licensplate);
            }
        }

    }
}