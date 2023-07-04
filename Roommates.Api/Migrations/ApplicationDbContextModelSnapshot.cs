﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Roommates.Api.Data;

#nullable disable

namespace Roommates.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("roomates")
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Roommates.Infrastructure.Models.DynamicFeature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long?>("Count")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DynamicFeatureType")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.Property<bool?>("IsExist")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("DynamicFeatures", "roomates");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("VerificationCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("VerifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Emails", "roomates");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Content")
                        .HasColumnType("bytea");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EntityState")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.Property<string>("Extension")
                        .HasColumnType("text");

                    b.Property<Guid?>("InactivatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("InactivatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsTemporary")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MimeType")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Files", "roomates");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.FilePost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<short?>("Sequence")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("PostId");

                    b.ToTable("FilePost", "roomates");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

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

            modelBuilder.Entity("Roommates.Infrastructure.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EntityState")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.Property<Guid?>("InactivatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("InactivatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<string>("PostStatus")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("ViewsCount")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("LocationId");

                    b.ToTable("Posts", "roomates");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.StaticFeatures", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CurrencyType")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.Property<string>("EntityState")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.Property<DateTime?>("InactivatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsForSelling")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<string>("PreferedClientType")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("PricePeriodType")
                        .HasColumnType("varchar(24)");

                    b.Property<short>("RoomsCount")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("PostId")
                        .IsUnique();

                    b.ToTable("StaticFeatures", "roomates");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("EmailVerifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EntityState")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("varchar(24)");

                    b.Property<DateTime?>("InactivatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<DateTime?>("PhoneNumberVerifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ProfilePictureFileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProfilePictureFileId");

                    b.ToTable("Users", "roomates");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.UserPost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserPostRelationType")
                        .IsRequired()
                        .HasColumnType("varchar(24)");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPosts", "roomates");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.DynamicFeature", b =>
                {
                    b.HasOne("Roommates.Infrastructure.Models.Post", "Post")
                        .WithMany("DynamicFeatures")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.Email", b =>
                {
                    b.HasOne("Roommates.Infrastructure.Models.User", "User")
                        .WithMany("EmailVerifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.File", b =>
                {
                    b.HasOne("Roommates.Infrastructure.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.FilePost", b =>
                {
                    b.HasOne("Roommates.Infrastructure.Models.File", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Roommates.Infrastructure.Models.Post", "Post")
                        .WithMany("AppartmentViewFiles")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.Post", b =>
                {
                    b.HasOne("Roommates.Infrastructure.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Roommates.Infrastructure.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.StaticFeatures", b =>
                {
                    b.HasOne("Roommates.Infrastructure.Models.Post", "Post")
                        .WithOne("StaticFeatures")
                        .HasForeignKey("Roommates.Infrastructure.Models.StaticFeatures", "PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.User", b =>
                {
                    b.HasOne("Roommates.Infrastructure.Models.File", "ProfilePicture")
                        .WithMany()
                        .HasForeignKey("ProfilePictureFileId");

                    b.Navigation("ProfilePicture");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.UserPost", b =>
                {
                    b.HasOne("Roommates.Infrastructure.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Roommates.Infrastructure.Models.User", "User")
                        .WithMany("RelatedPosts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.Post", b =>
                {
                    b.Navigation("AppartmentViewFiles");

                    b.Navigation("DynamicFeatures");

                    b.Navigation("StaticFeatures");
                });

            modelBuilder.Entity("Roommates.Infrastructure.Models.User", b =>
                {
                    b.Navigation("EmailVerifications");

                    b.Navigation("RelatedPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
