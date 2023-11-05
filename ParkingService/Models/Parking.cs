namespace ParkingService.Models
{
    public class Parking
    {
        public int Id { get; set; }
        public string Licensplate { get; set; }
        public DateTime Time { get; set; }
        public string Parkinglot { get; set; }
        public string? Phonenumber { get; set; }
        public string? Email { get; set; }

        public Parking(string licensplate)
        {
            Licensplate = licensplate;
        }
    }
}
