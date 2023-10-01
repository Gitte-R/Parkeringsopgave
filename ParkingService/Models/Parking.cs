namespace ParkingService.Models
{
    public class Parking
    {
        public string Licenseplate { get; set; }
        public DateTime Time { get; set; }
        public string Parkinglot { get; set; }
        public string? Phonenumber { get; set; }
        public string? Email { get; set; }

        public Parking(string licenseplate)
        {
            Licenseplate = licenseplate;
        }
    }
}
