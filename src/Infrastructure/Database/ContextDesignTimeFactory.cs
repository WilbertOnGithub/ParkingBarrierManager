using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Arentheym.ParkingBarrier.Infrastructure.Database;

public class ContextDesignTimeFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        IConfiguration configuration = GetDevelopmentConfiguration();

        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseSqlite(
            configuration.GetSection("DatabaseConfiguration").GetValue<string>("ConnectionString")
        );

        return new DatabaseContext(optionsBuilder.Options);

        IConfiguration GetDevelopmentConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: false)
                .Build();
        }
    }
}
