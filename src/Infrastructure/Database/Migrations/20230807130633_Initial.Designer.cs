﻿// <auto-generated />
using System;
using Arentheym.ParkingBarrier.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Arentheym.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230807130633_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("ApartmentConfigurationIntercom", b =>
                {
                    b.Property<string>("ApartmentConfigurationId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IntercomsId")
                        .HasColumnType("TEXT");

                    b.HasKey("ApartmentConfigurationId", "IntercomsId");

                    b.HasIndex("IntercomsId");

                    b.ToTable("ApartmentConfigurationIntercoms", (string)null);
                });

            modelBuilder.Entity("Arentheym.ParkingBarrier.Domain.ApartmentConfiguration", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("AccessCode")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("AccessCode");

                    b.Property<bool>("DialToOpen")
                        .HasColumnType("INTEGER")
                        .HasColumnName("DialToOpen");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<short>("MemoryLocation")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MemoryLocation");

                    b.HasKey("Id");

                    b.ToTable("ApartmentConfigurations", (string)null);
                });

            modelBuilder.Entity("Arentheym.ParkingBarrier.Domain.Intercom", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("MasterCode")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("TEXT")
                        .HasColumnName("MasterCode");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Intercoms", (string)null);
                });

            modelBuilder.Entity("ApartmentConfigurationIntercom", b =>
                {
                    b.HasOne("Arentheym.ParkingBarrier.Domain.ApartmentConfiguration", null)
                        .WithMany()
                        .HasForeignKey("ApartmentConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Arentheym.ParkingBarrier.Domain.Intercom", null)
                        .WithMany()
                        .HasForeignKey("IntercomsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Arentheym.ParkingBarrier.Domain.ApartmentConfiguration", b =>
                {
                    b.OwnsMany("Arentheym.ParkingBarrier.Domain.DivertPhoneNumber", "PhoneNumbers", b1 =>
                        {
                            b1.Property<string>("ApartmentConfigurationId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Order")
                                .HasColumnType("INTEGER")
                                .HasColumnName("Order");

                            b1.Property<string>("Number")
                                .HasColumnType("TEXT")
                                .HasColumnName("Number");

                            b1.HasKey("ApartmentConfigurationId", "Order", "Number");

                            b1.ToTable("ApartmentConfigurationPhoneNumbers", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ApartmentConfigurationId");
                        });

                    b.Navigation("PhoneNumbers");
                });

            modelBuilder.Entity("Arentheym.ParkingBarrier.Domain.Intercom", b =>
                {
                    b.OwnsOne("Arentheym.ParkingBarrier.Domain.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<Guid>("IntercomId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("TEXT")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("IntercomId");

                            b1.ToTable("Intercoms");

                            b1.WithOwner()
                                .HasForeignKey("IntercomId");
                        });

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
