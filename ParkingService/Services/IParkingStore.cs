using ParkingService.Models;

namespace ParkingService.Services
{
    public interface IParkingStore
    {
        Parking Get(string licenseplate);
        void Save(string licenseplate, string parkinglot, string? phonenumber, string? email);
        void Remove(string licenseplate);
    }
}
