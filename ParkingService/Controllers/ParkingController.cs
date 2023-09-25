using Microsoft.AspNetCore.Mvc;
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

        public ParkingController(IParkingStore parkingStore)
        {
            this.parkingStore = parkingStore;
        }

        [HttpGet("{licenseplate}")]
        public Parking Get(string licenseplate) 
        {
            return parkingStore.Get(licenseplate);
        }


        [HttpPost("RegisterParking")]
        public Parking Post(string licenseplate, string parkinglot, int? phonenumber, string? email)
        {
            Parking NewParking = new(licenseplate);
            {
                NewParking.Parkinglot = parkinglot;
                NewParking.Time = DateTime.Now;
                NewParking.Phonenumber = phonenumber;
                NewParking.Email = email;
            };
            parkingStore.Save(NewParking);
            return NewParking;
        }


        [HttpDelete(Name = "DeleteParking{licenseplate}")]
        public void Delete(string licenseplate)
        {
            parkingStore.Remove(licenseplate);
        }
    }
}
