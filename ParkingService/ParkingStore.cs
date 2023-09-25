using System.Linq;

namespace ParkingService
{
    public class ParkingStore : IParkingStore
    {
        private static readonly Dictionary<string, Parking> ParkingsDatabase = new Dictionary<string, Parking>();
        public Parking Get(string licenseplate) => ParkingsDatabase.ContainsKey(licenseplate) ? ParkingsDatabase[licenseplate] : new Parking(licenseplate);

        public void Save(Parking parkering) => ParkingsDatabase[parkering.Licenseplate] = parkering;

        public void Remove(string licenseplate)
        {
            if (ParkingsDatabase.ContainsKey(licenseplate))
            {
                ParkingsDatabase.Remove(licenseplate);
            }
        }

    }
}