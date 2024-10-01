﻿// <auto-generated />
using System;
using Jeux_Olympiques.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Jeux_Olympiques.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Jeux_Olympiques.Areas.Identity.Data.Jeux_OlympiquesUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AccountKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("Jeux_OlympiquesUser");

                    b.UseTphMappingStrategy();

                    b.HasData(
                        new
                        {
                            Id = "1DBFDCBB-4D11-4562-9756-602889FD4163",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "05e593f3-cb5b-4597-a926-e9bbf804289f",
                            Email = "admin@jeuxolympiques.com",
                            EmailConfirmed = true,
                            FirstName = "AdminPrenom",
                            LastName = "AdminNom",
                            LockoutEnabled = true,
                            NormalizedEmail = "ADMIN@JEUXOLYMPIQUES.COM",
                            NormalizedUserName = "ADMIN@JEUXOLYMPIQUES.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEG1swvJSZyrab4nmV8Dl1AaNZIz/Owlf9869lZ7X0Vxr3DCd/XzSutmpTNTMddrNjA==",
                            PhoneNumber = "NULL",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "65K4MQIYK4OSUVYHUY46KRXZ3EUBNO6R",
                            TwoFactorEnabled = false,
                            UserName = "admin@jeuxolympiques.com"
                        },
                        new
                        {
                            Id = "c7a42f97-4108-4ff9-bdee-326866513a03",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "3967ca6c-926b-4610-93f8-e4ee58de29ef",
                            Email = "user@jeuxolympiques.com",
                            EmailConfirmed = true,
                            FirstName = "Franck",
                            LastName = "Letesteur",
                            LockoutEnabled = true,
                            NormalizedEmail = "USER@JEUXOLYMPIQUES.COM",
                            NormalizedUserName = "USER@JEUXOLYMPIQUES.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEAuQECgPLnpmQ3uGf3DY87bFAf1ny8toJ0OsyDbLnnruDv3QdxbYUe273bLsX39Eeg==",
                            PhoneNumber = "NULL",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "LZBL3DTGYNXJPNQ3ZJ2JN3ZNI54WA4T3",
                            TwoFactorEnabled = false,
                            UserName = "user@jeuxolympiques.com"
                        });
                });

            modelBuilder.Entity("Jeux_Olympiques.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Entitled")
                        .HasColumnType("int");

                    b.Property<string>("Presentation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Site")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 4,
                            Date = new DateTime(2024, 9, 11, 10, 15, 0, 0, DateTimeKind.Unspecified),
                            Entitled = 3,
                            Presentation = "8ème de finale Escrime Homme",
                            Site = 3
                        },
                        new
                        {
                            Id = 5,
                            Date = new DateTime(2024, 9, 12, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            Entitled = 2,
                            Presentation = "Quart de finale de Natation Femme 500 mètres",
                            Site = 4
                        },
                        new
                        {
                            Id = 6,
                            Date = new DateTime(2024, 9, 11, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            Entitled = 7,
                            Presentation = "Finale Homme France - Chine",
                            Site = 3
                        });
                });

            modelBuilder.Entity("Jeux_Olympiques.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Place")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PublishId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("PublishId");

                    b.ToTable("Offers");

                    b.HasData(
                        new
                        {
                            Id = 14,
                            Description = "Entrée pour 1 personne",
                            EventId = 4,
                            Place = "Placement libre",
                            Price = 29m,
                            Title = "OFFRE SOLO"
                        },
                        new
                        {
                            Id = 15,
                            Description = "Entrée pour 2 personnes",
                            EventId = 4,
                            Place = "Placement libre",
                            Price = 55m,
                            Title = "OFFRE DUO"
                        },
                        new
                        {
                            Id = 16,
                            Description = "Entrée pour 4 personnes",
                            EventId = 4,
                            Place = "Placement libre",
                            Price = 100m,
                            Title = "OFFRE FAMILLE"
                        });
                });

            modelBuilder.Entity("Jeux_Olympiques.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BuyerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ContainsId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("QrCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TicketKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("ContainsId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "59C78292-150C-4218-B08A-ACED1F79A45B",
                            ConcurrencyStamp = "F92315DD - 48DD - 4EA1 - A4C3 - C532D81843B6",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "1DBFDCBB-4D11-4562-9756-602889FD4163",
                            RoleId = "59C78292-150C-4218-B08A-ACED1F79A45B"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Jeux_Olympiques.Models.User", b =>
                {
                    b.HasBaseType("Jeux_Olympiques.Areas.Identity.Data.Jeux_OlympiquesUser");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Jeux_Olympiques.Models.Event", b =>
                {
                    b.HasOne("Jeux_Olympiques.Models.User", "Created")
                        .WithMany("PublishEvent")
                        .HasForeignKey("CreatedId");

                    b.Navigation("Created");
                });

            modelBuilder.Entity("Jeux_Olympiques.Models.Offer", b =>
                {
                    b.HasOne("Jeux_Olympiques.Models.Event", "Events")
                        .WithMany("Offers")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jeux_Olympiques.Models.User", "Publish")
                        .WithMany("CreateOffer")
                        .HasForeignKey("PublishId");

                    b.Navigation("Events");

                    b.Navigation("Publish");
                });

            modelBuilder.Entity("Jeux_Olympiques.Models.Ticket", b =>
                {
                    b.HasOne("Jeux_Olympiques.Models.User", "Buyer")
                        .WithMany("BuyTicket")
                        .HasForeignKey("BuyerId");

                    b.HasOne("Jeux_Olympiques.Models.Offer", "Contains")
                        .WithMany("IsContained")
                        .HasForeignKey("ContainsId");

                    b.Navigation("Buyer");

                    b.Navigation("Contains");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Jeux_Olympiques.Areas.Identity.Data.Jeux_OlympiquesUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Jeux_Olympiques.Areas.Identity.Data.Jeux_OlympiquesUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jeux_Olympiques.Areas.Identity.Data.Jeux_OlympiquesUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Jeux_Olympiques.Areas.Identity.Data.Jeux_OlympiquesUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Jeux_Olympiques.Models.Event", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("Jeux_Olympiques.Models.Offer", b =>
                {
                    b.Navigation("IsContained");
                });

            modelBuilder.Entity("Jeux_Olympiques.Models.User", b =>
                {
                    b.Navigation("BuyTicket");

                    b.Navigation("CreateOffer");

                    b.Navigation("PublishEvent");
                });
#pragma warning restore 612, 618
        }
    }
}
