using EventService.Serivces;
using Microsoft.AspNetCore.Mvc;
using ParkingService.Models;
using ParkingService.Services;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.Linq;


namespace ParkingService.Controllers
{
    [Route("/parking")]
    //[ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingStore parkingStore;
        private readonly IEventStore eventStore;

        public ParkingController(IParkingStore parkingStore, IEventStore eventstore)
        {
            this.parkingStore = parkingStore;
            this.eventStore = eventstore;
        }

        [HttpGet("{licenseplate}")]
        public Parking Get(string licenseplate) 
        {
            return parkingStore.Get(licenseplate);
        }


        [HttpPost("RegisterParking")]
        public void Post(string licenseplate, string parkinglot, string? phonenumber, string? email)
        {
            parkingStore.Save(licenseplate, parkinglot, phonenumber, email);
        }


        [HttpDelete(Name = "DeleteParking{licenseplate}")]
        public void Delete(string licenseplate)
        {
            parkingStore.Remove(licenseplate);
        }
    }
}
