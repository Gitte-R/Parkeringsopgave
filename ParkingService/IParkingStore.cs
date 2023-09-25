namespace ParkingService
{
    public interface IParkingStore
    {
        Parking Get(string licenseplate);
        void Save(Parking parking);
        void Remove(string licenseplate);
    }
}
