using System;
using DeadFishStudio.Product.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeadFishStudio.Product.Infrastructure.CrossCutting
{
    internal static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services,
            ProductConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<ProductContext>(options =>
                {
                    options
                        .UseLazyLoadingProxies()
                        .UseSqlServer(configuration.ConnectionString);
                });

            return services;
        }
    }
}