using Microsoft.EntityFrameworkCore;
using ParkingService.Models;
using ParkingService.Events;

namespace ParkingService.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Parking> Parking { get; set; }


    }
}
