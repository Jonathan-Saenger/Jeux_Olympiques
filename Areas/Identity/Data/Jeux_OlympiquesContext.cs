using Jeux_Olympiques.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jeux_Olympiques.Data;

public class Jeux_OlympiquesContext : IdentityDbContext<Jeux_OlympiquesUser>
{
    public Jeux_OlympiquesContext(DbContextOptions<Jeux_OlympiquesContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
