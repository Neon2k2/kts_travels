using kts_travels.Configuration;
using kts_travels.Models;
using Microsoft.EntityFrameworkCore;

namespace kts_travels.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<DieselEntry> DieselEntries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DieselEntryConfiguration());


        }
    }

    
}
