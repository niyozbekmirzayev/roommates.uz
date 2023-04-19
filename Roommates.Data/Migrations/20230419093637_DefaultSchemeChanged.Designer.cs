﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Roommates.Data;

#nullable disable

namespace Roommates.Data.Migrations
{
    [DbContext(typeof(RoommatesDbContext))]
    [Migration("20230419093637_DefaultSchemeChanged")]
    partial class DefaultSchemeChanged
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("roomates")
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PostRoommate", b =>
                {
                    b.Property<Guid>("LikedByRoommatesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LikedPostsId")
                        .HasColumnType("uuid");

                    b.HasKey("LikedByRoommatesId", "LikedPostsId");

                    b.HasIndex("LikedPostsId");

                    b.ToTable("PostRoommate", "roomates");
                });

            modelBuilder.Entity("Roommates.Domain.Models.Files.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorRoommateId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EntityState")
                        .HasColumnType("integer");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Files", "roomates");
                });

            modelBuilder.Entity("Roommates.Domain.Models.Locations.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EntityState")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Locations", "roomates");
                });

            modelBuilder.Entity("Roommates.Domain.Models.Posts.FilesPosts", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("FilesPosts", "roomates");
                });

            modelBuilder.Entity("Roommates.Domain.Models.Posts.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedByRoommateId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CurrencyType")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EntityState")
                        .HasColumnType("integer");

                    b.Property<bool>("IsForSelling")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<int>("PreferedRoommateGender")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("PricePeriodType")
                        .HasColumnType("integer");

                    b.Property<short>("RoomsCount")
                        .HasColumnType("smallint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("ViewedTime")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByRoommateId");

                    b.HasIndex("LocationId");

                    b.ToTable("Posts", "roomates");
                });

            modelBuilder.Entity("Roommates.Domain.Models.Roommates.Roommate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EntityState")
                        .HasColumnType("integer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Gender")
                        .HasColumnType("integer");

                    b.Property<bool>("IsPhoneNumberVerified")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ProfilePictureFileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Roommates", "roomates");
                });

            modelBuilder.Entity("PostRoommate", b =>
                {
                    b.HasOne("Roommates.Domain.Models.Roommates.Roommate", null)
                        .WithMany()
                        .HasForeignKey("LikedByRoommatesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Roommates.Domain.Models.Posts.Post", null)
                        .WithMany()
                        .HasForeignKey("LikedPostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Roommates.Domain.Models.Posts.FilesPosts", b =>
                {
                    b.HasOne("Roommates.Domain.Models.Posts.Post", null)
                        .WithMany("AppartmentViewFiles")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Roommates.Domain.Models.Posts.Post", b =>
                {
                    b.HasOne("Roommates.Domain.Models.Roommates.Roommate", "CreatedByRoommate")
                        .WithMany("OwnPosts")
                        .HasForeignKey("CreatedByRoommateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Roommates.Domain.Models.Locations.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByRoommate");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Roommates.Domain.Models.Posts.Post", b =>
                {
                    b.Navigation("AppartmentViewFiles");
                });

            modelBuilder.Entity("Roommates.Domain.Models.Roommates.Roommate", b =>
                {
                    b.Navigation("OwnPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
