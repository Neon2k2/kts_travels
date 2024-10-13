using kts_travels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kts_travels.WebAPI.Configuration
{
    public class TripLogConfiguration : IEntityTypeConfiguration<TripLog>
    {
        public void Configure(EntityTypeBuilder<TripLog> builder)
        {
            builder.ToTable("TripLog");

            builder.Property(t => t.DieselLiters)
        .HasColumnType("decimal(18, 2)");
        }


    }
}
