using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FlavorTreat.Models
{
  public class FlavorTreatContextFactory : IDesignTimeDbContextFactory<FlavorTreatContext>
  {

    FlavorTreatContext IDesignTimeDbContextFactory<FlavorTreatContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<FlavorTreatContext>();

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));

      return new FlavorTreatContext(builder.Options);
    }
  }
}