﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using kts_travels.Infrastructure.Persistence;

#nullable disable

namespace kts_travels.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241009233325_newDB")]
    partial class newDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("kts_travels.Domain.Entities.Site", b =>
                {
                    b.Property<int>("SiteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SiteId"));

                    b.Property<string>("SiteName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SiteId");

                    b.ToTable("Sites");
                });

            modelBuilder.Entity("kts_travels.Domain.Entities.TripLog", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TripId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DieselLiters")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("StartingKm")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.Property<string>("VehicleNO")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.HasKey("TripId");

                    b.HasIndex("LocationId");

                    b.HasIndex("VehicleId");

                    b.ToTable("TripLog", (string)null);
                });

            modelBuilder.Entity("kts_travels.Domain.Entities.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleNo")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.HasKey("VehicleId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("kts_travels.Domain.Entities.VehicleSummary", b =>
                {
                    b.Property<int>("SummaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SummaryId"));

                    b.Property<decimal>("Average")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ClosingKms")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Month")
                        .HasColumnType("datetime2");

                    b.Property<int>("OpeningKms")
                        .HasColumnType("int");

                    b.Property<int>("SRNo")
                        .HasColumnType("int");

                    b.Property<int>("TotalDaysFilledDiesel")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalDiesel")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TotalKmRun")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("SummaryId");

                    b.HasIndex("LocationId");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleSummary", (string)null);
                });

            modelBuilder.Entity("kts_travels.Domain.Entities.TripLog", b =>
                {
                    b.HasOne("kts_travels.Domain.Entities.Site", "Location")
                        .WithMany("TripLogs")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("kts_travels.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany("TripLogs")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("kts_travels.Domain.Entities.VehicleSummary", b =>
                {
                    b.HasOne("kts_travels.Domain.Entities.Site", "Location")
                        .WithMany("VehicleSummaries")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("kts_travels.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("kts_travels.Domain.Entities.Site", b =>
                {
                    b.Navigation("TripLogs");

                    b.Navigation("VehicleSummaries");
                });

            modelBuilder.Entity("kts_travels.Domain.Entities.Vehicle", b =>
                {
                    b.Navigation("TripLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
