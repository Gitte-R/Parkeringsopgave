using ParkingService.Models;
using ParkingService.Events;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Hosting;
using ParkingService.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Polly;

namespace ParkingService.Services
{
    public class ParkingStore : IParkingStore
    {
        private readonly IEventStore eventStore;
        private readonly ApplicationDBContext applicationDBContext;

        public ParkingStore(IEventStore eventStore, ApplicationDBContext applicationDBContext)
        {
            this.eventStore = eventStore;
            this.applicationDBContext = applicationDBContext;
        }

        //private static readonly Dictionary<string, Parking> ParkingsDatabase = new Dictionary<string, Parking>();

        //public Parking Get(string licensplate) => ParkingsDatabase.ContainsKey(licensplate) ? ParkingsDatabase[licensplate] : new Parking(licensplate);


        public async Task<Parking> GetParking(string licensplate)
        {
            List<Parking> parkinglist = await applicationDBContext.Parking.ToListAsync();
            Parking parking = (Parking)parkinglist.Where(m => m.Licensplate == licensplate);

            if (parking is not null)
            {
                return parking;
            }
            else
            {
                return new Parking(licensplate);
            }

        }
    
        public async void SaveParking(string licensplate, string parkinglot, string? phonenumber, string? email) 
        {
            Parking NewParking = new Parking(licensplate)
            {
                Parkinglot = parkinglot,
                Time = DateTime.Now,
                Phonenumber = phonenumber,
                Email = email
            };

            this.eventStore.RaiseEvent("ParkingStarted", NewParking);
            
            await applicationDBContext.Parking.AddAsync(NewParking);
            //await applicationDBContext.SaveChangesAsync();
          
            
            //ParkingsDatabase[NewParking.Licensplate] = NewParking;
        }

        public async void RemoveParking(string licensplate)
        {
            List<Parking> parkinglist = await applicationDBContext.Parking.ToListAsync();
            Parking parking = (Parking)parkinglist.Where(m => m.Licensplate == licensplate);

            if (parking is not null)
            {
                this.eventStore.RaiseEvent("ParkingEnded", parking);

                applicationDBContext.Parking.Remove(parking);
            }


            //if (ParkingsDatabase.ContainsKey(licensplate))
            //{
            //    ParkingsDatabase.Remove(licensplate);
            //}
        }

    }
}