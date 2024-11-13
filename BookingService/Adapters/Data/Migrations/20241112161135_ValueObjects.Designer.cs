﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(HotelDbContext))]
    [Migration("20241112161135_ValueObjects")]
    partial class ValueObjects
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Bookings.Entities.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GuestId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PlacedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("RoomId");

                    b.ToTable("Bookings", (string)null);
                });

            modelBuilder.Entity("Domain.Guests.Entities.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("Domain.Rooms.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsInMaintenance")
                        .HasColumnType("boolean");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Domain.Bookings.Entities.Booking", b =>
                {
                    b.HasOne("Domain.Guests.Entities.Guest", "Guest")
                        .WithMany()
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Rooms.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Guest");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Domain.Guests.Entities.Guest", b =>
                {
                    b.OwnsOne("Domain.Guests.ValueObjects.PersonId", "DocumentId", b1 =>
                        {
                            b1.Property<int>("GuestId")
                                .HasColumnType("integer");

                            b1.Property<int>("DocumentType")
                                .HasColumnType("integer");

                            b1.Property<string>("IdNumber")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("GuestId");

                            b1.ToTable("Guests");

                            b1.WithOwner()
                                .HasForeignKey("GuestId");
                        });

                    b.Navigation("DocumentId");
                });

            modelBuilder.Entity("Domain.Rooms.Entities.Room", b =>
                {
                    b.OwnsOne("Domain.Rooms.ValueObjects.Price", "Price", b1 =>
                        {
                            b1.Property<int>("RoomId")
                                .HasColumnType("integer");

                            b1.Property<int>("Currency")
                                .HasColumnType("integer");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric");

                            b1.HasKey("RoomId");

                            b1.ToTable("Rooms");

                            b1.WithOwner()
                                .HasForeignKey("RoomId");
                        });

                    b.Navigation("Price")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
