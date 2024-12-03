﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WheelDeal.Domain.Database;

#nullable disable

namespace WheelDeal.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WheelDeal.Domain.Database.ModelsDb.CarDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("body");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("brand");

                    b.Property<decimal>("EngineValue")
                        .HasColumnType("numeric")
                        .HasColumnName("enginevalue");

                    b.Property<string>("Fuel")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("fuel");

                    b.Property<decimal>("FuelConsumption")
                        .HasColumnType("numeric")
                        .HasColumnName("fuelconsumption");

                    b.Property<int>("Mileage")
                        .HasColumnType("integer")
                        .HasColumnName("mileage");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("model");

                    b.Property<int>("PlacesCount")
                        .HasColumnType("integer")
                        .HasColumnName("placescount");

                    b.Property<int>("Power")
                        .HasColumnType("integer")
                        .HasColumnName("power");

                    b.Property<string>("Transmission")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("transmission");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("Id");

                    b.ToTable("cars", (string)null);
                });

            modelBuilder.Entity("WheelDeal.Domain.Database.ModelsDb.CategoryDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("CountPosts")
                        .HasColumnType("integer")
                        .HasColumnName("countposts");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("imagepath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("WheelDeal.Domain.Database.ModelsDb.PostDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("AvailabilityStatus")
                        .HasColumnType("boolean")
                        .HasColumnName("availabilitystatus");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uuid")
                        .HasColumnName("carid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("categoryid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createdat");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.HasKey("Id");

                    b.HasIndex("CarId");
                    b.HasIndex("CategoryId");

                    b.ToTable("posts");
                });

            modelBuilder.Entity("WheelDeal.Domain.Database.ModelsDb.RateDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<int>("Points")
                        .HasColumnType("integer")
                        .HasColumnName("points");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid")
                        .HasColumnName("postid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("userid");

                    b.Property<Guid>("postid")
                        .HasColumnType("uuid");

                    b.Property<Guid>("userid")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("postid");

                    b.HasIndex("userid");

                    b.ToTable("rates", null, t =>
                        {
                            t.Property("postid")
                                .HasColumnName("postid1");

                            t.Property("userid")
                                .HasColumnName("userid1");
                        });
                });

            modelBuilder.Entity("WheelDeal.Domain.Database.ModelsDb.UserDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createdat");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("pathimage");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("WheelDeal.Domain.Database.ModelsDb.PostDb", b =>
                {
                    b.HasOne("WheelDeal.Domain.Database.ModelsDb.CarDb", "Car")
                        .WithMany("Posts")
                        .HasForeignKey("carid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WheelDeal.Domain.Database.ModelsDb.CategoryDb", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("categoryid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WheelDeal.Domain.Database.ModelsDb.RateDb", b =>
                {
                    b.HasOne("WheelDeal.Domain.Database.ModelsDb.PostDb", "Post")
                        .WithMany("Rates")
                        .HasForeignKey("postid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WheelDeal.Domain.Database.ModelsDb.UserDb", "User")
                        .WithMany("Rates")
                        .HasForeignKey("userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WheelDeal.Domain.Database.ModelsDb.CarDb", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("WheelDeal.Domain.Database.ModelsDb.CategoryDb", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("WheelDeal.Domain.Database.ModelsDb.PostDb", b =>
                {
                    b.Navigation("Rates");
                });

            modelBuilder.Entity("WheelDeal.Domain.Database.ModelsDb.UserDb", b =>
                {
                    b.Navigation("Rates");
                });
#pragma warning restore 612, 618
        }
    }
}
