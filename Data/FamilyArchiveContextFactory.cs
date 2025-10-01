using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FamilyArchiveBackend.Data
{
    public class FamilyArchiveContextFactory : IDesignTimeDbContextFactory<FamilyArchiveContext>
    {
        public FamilyArchiveContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true);

            if (environment == "Development")
            {
                builder.AddUserSecrets<FamilyArchiveContextFactory>();
            }

            var configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<FamilyArchiveContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);

            return new FamilyArchiveContext(optionsBuilder.Options);
        }
    }
}