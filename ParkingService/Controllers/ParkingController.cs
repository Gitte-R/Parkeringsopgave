﻿using Microsoft.AspNetCore.Mvc;
using ParkingService.Models;
using ParkingService.Services;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.Linq;


namespace ParkingService.Controllers
{
    [Route("/parking")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingStore parkingStore;

        public ParkingController(IParkingStore parkingStore)
        {
            this.parkingStore = parkingStore;
        }

        [HttpGet("{licensplate}")]
        public Parking Get(string licensplate) 
        {
            return parkingStore.Get(licensplate);
        }


        [HttpPost("RegisterParking")]
        public void Post(string licensplate, string parkinglot, string? phonenumber, string? email)
        {
            parkingStore.Save(licensplate, parkinglot, phonenumber, email);
        }


        [HttpDelete(Name = "DeleteParking{licensplate}")]
        public void Delete(string licensplate)
        {
            parkingStore.Remove(licensplate);
        }
    }
}
