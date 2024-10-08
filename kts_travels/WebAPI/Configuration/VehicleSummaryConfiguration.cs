using kts_travels.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kts_travels.WebAPI.Configuration
{
    public class VehicleSummaryConfiguration : IEntityTypeConfiguration<VehicleSummary>
    {
        public void Configure(EntityTypeBuilder<VehicleSummary> builder)
        {
            builder.ToTable("VehicleSummary");

            builder.Property(t => t.TotalDiesel)
                .HasColumnType("decimal(18,2)");
            builder.Property(t => t.Average)
                .HasColumnType("decimal(18,2)");
        }
    }
}
