using kts_travels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace kts_travels.Configuration
{
    public class DieselEntryConfiguration: IEntityTypeConfiguration<DieselEntry>
    {
        public void Configure(EntityTypeBuilder<DieselEntry> builder)
        {
            builder.ToTable("DieselEntries");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Location)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(x => x.Date)
                .IsRequired();
            builder.Property(x => x.VehicleNo)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.DieselLiters)
                .IsRequired();
            builder.Property(x => x.StartingKm)
                .IsRequired();
        }
    }
}
