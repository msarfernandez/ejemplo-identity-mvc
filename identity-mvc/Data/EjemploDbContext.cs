using identity_mvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace identity_mvc.Data
{
    public class EjemploDbContext : IdentityDbContext
    {
        public EjemploDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Auto> Autos { get; set; }
    }
}
