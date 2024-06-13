﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestApp.Data;

#nullable disable

namespace TestApp.Migrations
{
    [DbContext(typeof(SensorDataContext))]
    partial class SensorDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.31");

            modelBuilder.Entity("TestApp.Data.PacketData", b =>
                {
                    b.Property<string>("Serial")
                        .HasColumnType("TEXT");

                    b.Property<double?>("AccelerationStDev")
                        .HasColumnType("REAL");

                    b.Property<int?>("Altitude")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Datetime")
                        .HasColumnType("TEXT");

                    b.Property<double?>("DewPoint")
                        .HasColumnType("REAL");

                    b.Property<double?>("EnvTemperature")
                        .HasColumnType("REAL");

                    b.Property<double?>("Humidity")
                        .HasColumnType("REAL");

                    b.Property<int?>("KSP")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double?>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<double?>("Pitch")
                        .HasColumnType("REAL");

                    b.Property<int?>("PrecipitationElaps")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("PrecipitationIntensity")
                        .HasColumnType("REAL");

                    b.Property<int?>("PrecipitationPeriod")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("PrecipitationQuantity")
                        .HasColumnType("REAL");

                    b.Property<int?>("PrecipitationType")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Pressure_hPa")
                        .HasColumnType("REAL");

                    b.Property<double?>("Pressure_mm_hg")
                        .HasColumnType("REAL");

                    b.Property<double?>("Roll")
                        .HasColumnType("REAL");

                    b.Property<double?>("SupplyVoltage")
                        .HasColumnType("REAL");

                    b.Property<double?>("VisibleRange")
                        .HasColumnType("REAL");

                    b.Property<int?>("WeAreFine")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("WindDirection")
                        .HasColumnType("REAL");

                    b.Property<double?>("WindSpeed")
                        .HasColumnType("REAL");

                    b.Property<double?>("WindVSound")
                        .HasColumnType("REAL");

                    b.HasKey("Serial");

                    b.ToTable("PacketData");
                });

            modelBuilder.Entity("TestApp.Data.SensorData", b =>
                {
                    b.Property<string>("Serial")
                        .HasColumnType("TEXT");

                    b.Property<string>("PacketSerial")
                        .HasColumnType("TEXT");

                    b.HasKey("Serial");

                    b.HasIndex("PacketSerial");

                    b.ToTable("SensorData");
                });

            modelBuilder.Entity("TestApp.Data.SensorData", b =>
                {
                    b.HasOne("TestApp.Data.PacketData", "Packet")
                        .WithMany()
                        .HasForeignKey("PacketSerial");

                    b.Navigation("Packet");
                });
#pragma warning restore 612, 618
        }
    }
}
