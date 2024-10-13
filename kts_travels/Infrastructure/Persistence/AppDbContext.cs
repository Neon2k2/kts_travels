using kts_travels.SharedServices.Domain.Entities;
using kts_travels.SharedServices.Application.Configuration;
using Microsoft.EntityFrameworkCore;

namespace kts_travels.SharedServices.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TripLog> TripLogs { get; set; }
        public DbSet<VehicleSummary> VehicleSummaries { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Site> Sites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TripLogConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleSummariesConfiguration());
        }
    }


}
