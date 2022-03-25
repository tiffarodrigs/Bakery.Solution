using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FlavorTreat.Models
{
  public class FlavorTreatContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Flavor> Flovors{get; set;}
    public DbSet<Treat> Treats{get; set;}
    public DbSet<FlavorTreat> FlavorTreat {get; set;}

    public FlavorTreatContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}