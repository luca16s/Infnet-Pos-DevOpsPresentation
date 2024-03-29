﻿using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DeadFishStudio.Product.Infrastructure.Data.Context
{
    public class ProductContextDesignFactory : IDesignTimeDbContextFactory<ProductContext>
    {
        public ProductContext CreateDbContext(string[] args)
        {
            // Get environment
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../DeadFishStudio.Product.Application.Api"))
                .AddJsonFile("appsettings.json", false, true)
                //.AddJsonFile($"appsettings.{environment}.json", true)
                //.AddEnvironmentVariables()
                .Build();

            // Get connection string
            var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();
            var connectionString = config.GetConnectionString(nameof(ProductContext));
            optionsBuilder.UseSqlServer(
                @"Server=mssql,1433;Initial Catalog=ProductDb;User Id=sa;Password=Alaska2017");
            return new ProductContext(optionsBuilder.Options);
        }
    }
}