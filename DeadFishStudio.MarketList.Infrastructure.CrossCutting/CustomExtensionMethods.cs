using System;
using DeadFishStudio.MarketList.Infrastructure.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace DeadFishStudio.MarketList.Infrastructure.CrossCutting
{
    internal static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services,
            MarketListConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<MarketListContext>(options =>
                {
                    options
                        .UseSqlServer(configuration.ConnectionString);
                });

            return services;
        }
    }
}