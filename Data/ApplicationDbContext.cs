using Jeux_Olympiques.Areas.Identity.Data;
using Jeux_Olympiques.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        }
    }
