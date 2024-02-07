using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    //Add-Migration InitialCreate -Context ApplicationContext -Project Infrastructure
    //Script-Migration -Context ApplicationContext -Project Infrastructure
    //Remove-Migration -Context ApplicationContext -Project Infrastructure
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        {
           
        }

        public DbSet<Catalog> Catalogs { get; set; }

        public DbSet<Note> Nodes { get; set; }
    }
}
