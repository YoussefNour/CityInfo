﻿// <auto-generated />
using CityInfo.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CityInfo.API.Migrations
{
    [DbContext(typeof(CityInfoContext))]
    [Migration("20230611182200_CityInfoDbInitialDataSeeding")]
    partial class CityInfoDbInitialDataSeeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("CityInfo.API.entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Capital of Egypt",
                            Name = "Cairo"
                        },
                        new
                        {
                            Id = 2,
                            Description = "largest shore country of Egypt",
                            Name = "Alexandria"
                        },
                        new
                        {
                            Id = 3,
                            Description = "South most city of Egypt",
                            Name = "Aswan"
                        });
                });

            modelBuilder.Entity("CityInfo.API.entities.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PointsOfInterest");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityId = 1,
                            Description = "great pyramids there are 3 of them",
                            Name = "Pyramids"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 1,
                            Description = "Sells best hawawshy and kebda",
                            Name = "Hawawshy Al Rabi3"
                        },
                        new
                        {
                            Id = 3,
                            CityId = 2,
                            Description = "Library of Alexandria",
                            Name = "Library of Alexandria"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 2,
                            Description = "Sells best kebda",
                            Name = "Kebdet El Falah"
                        });
                });

            modelBuilder.Entity("CityInfo.API.entities.PointOfInterest", b =>
                {
                    b.HasOne("CityInfo.API.entities.City", "City")
                        .WithMany("PointsOfInterest")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("CityInfo.API.entities.City", b =>
                {
                    b.Navigation("PointsOfInterest");
                });
#pragma warning restore 612, 618
        }
    }
}
