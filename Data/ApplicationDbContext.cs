using Jeux_Olympiques.Areas.Identity.Data;
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
    }
}
