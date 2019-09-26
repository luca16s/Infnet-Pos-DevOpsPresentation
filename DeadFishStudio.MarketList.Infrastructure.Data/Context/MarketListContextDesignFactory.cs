using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DeadFishStudio.MarketList.Infrastructure.Data.Context
{
    public class MarketListContextDesignFactory : IDesignTimeDbContextFactory<MarketListContext>
    {
        public MarketListContext CreateDbContext(string[] args)
        {
            // Get environment
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
                    "../DeadFishStudio.MarketList.Application.Api"))
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment}.json", true)
                .AddEnvironmentVariables()
                .Build();

            // Get connection string
            var optionsBuilder = new DbContextOptionsBuilder<MarketListContext>();
            var connectionString = config.GetConnectionString(nameof(MarketListContext));
            optionsBuilder.UseSqlServer("Data Source=localhost,5433;Initial Catalog=MarketListApiDB;User ID=SA;Password=Alaska2017;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return new MarketListContext(optionsBuilder.Options);
        }
    }
}