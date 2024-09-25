using Jeux_Olympiques.Areas.Identity.Data;
using Jeux_Olympiques.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security;
using System.Security.Policy;


namespace Jeux_Olympiques.Data
{
    public class ApplicationDbContext : IdentityDbContext<Jeux_OlympiquesUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Event>? Events { get; set; }
        public DbSet<Offer>? Offers { get; set; }
        public DbSet<Ticket>? Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Création du compte Admin
            modelBuilder.Entity<Jeux_OlympiquesUser>().HasData(
               new Jeux_OlympiquesUser
               {
                    Id = "1DBFDCBB-4D11-4562-9756-602889FD4163",
                    UserName = "admin@jeuxolympiques.com",
                    NormalizedUserName = "ADMIN@JEUXOLYMPIQUES.COM",
                    Email = "admin@jeuxolympiques.com",
                    NormalizedEmail = "ADMIN@JEUXOLYMPIQUES.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEG1swvJSZyrab4nmV8Dl1AaNZIz/Owlf9869lZ7X0Vxr3DCd/XzSutmpTNTMddrNjA==",
                    SecurityStamp = "65K4MQIYK4OSUVYHUY46KRXZ3EUBNO6R",
                    ConcurrencyStamp = "05e593f3-cb5b-4597-a926-e9bbf804289f",
                    PhoneNumber = "NULL",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    FirstName = "AdminPrenom",
                    LastName = "AdminNom",
               }
            );

            //Création du rôle admin
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "59C78292-150C-4218-B08A-ACED1F79A45B",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "F92315DD - 48DD - 4EA1 - A4C3 - C532D81843B6",
                }
            );

            //Intégration de l'Admin dans le rôle Admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "1DBFDCBB-4D11-4562-9756-602889FD4163",
                    RoleId = "59C78292-150C-4218-B08A-ACED1F79A45B",
                }
            );

            //Création des Events
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 4,
                    Entitled = "Tir à l'arc",
                    Presentation = "8ème de finale du Tir à l'Arc homme",
                    Date = DateTime.Parse("2024 - 09 - 11 10:15:00"),
                    Site = "Pelouse du Stade de Vincenne",
                },
                new Event
                {
                    Id = 5,
                    Entitled = "Natation",
                    Presentation = "Quart de finale de Natation Femme 500 mètres",
                    Date = DateTime.Parse("2024-09-12 12:30:00"),
                    Site = "Piscine du Parc des Princes",
                },
                new Event
                {
                    Id = 6,
                    Entitled = "Ping Pong",
                    Presentation = "Finale Homme France - Chine",
                    Date = DateTime.Parse("2024-09-11 18:00:00"),
                    Site = "Salle du Gymnase du Luxembourg",
                }
            );

            //Création des offres
            modelBuilder.Entity<Offer>().HasData(
                new Offer
                {
                    Id = 14,
                    Title = "OFFRE SOLO",
                    Description = "Entrée pour 1 personne",
                    Place = "Placement libre",
                    Price = 29,
                    EventId = 4,
                },
                new Offer
                {
                    Id = 15,
                    Title = "OFFRE DUO",
                    Description = "Entrée pour 2 personnes",
                    Place = "Placement libre",
                    Price = 55,
                    EventId = 4,
                },
                new Offer
                {
                    Id = 16,
                    Title = "OFFRE FAMILLE",
                    Description = "Entrée pour 4 personnes",
                    Place = "Placement libre",
                    Price = 100,
                    EventId = 4,
                }
            );
        }
    }
}
