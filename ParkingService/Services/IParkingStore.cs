using ParkingService.Models;

namespace ParkingService.Services
{
    public interface IParkingStore
    {
        Task<Parking> GetParking(string licenseplate);
        void SaveParking(string licenseplate, string parkinglot, string? phonenumber, string? email);
        void RemoveParking(string licenseplate);
    }
}
