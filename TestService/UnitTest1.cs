using ParkingService.Models;

namespace TestService

{
    public class UnitTest1
    {
        [Fact]
        public void TestParkingPParkinglotNotNull()
        {
            var parking = new Parking("DD45645");
            Assert.NotNull(parking.Parkinglot);
        }
    }
}