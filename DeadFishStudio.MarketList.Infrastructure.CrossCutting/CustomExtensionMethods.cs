using System;
using DeadFishStudio.MarketList.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace DeadFishStudio.MarketList.Infrastructure.CrossCutting
{
    internal static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services,
            MarketListConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.AddEntityFrameworkMySQL()
                .AddDbContext<MarketListContext>(options =>
                {
                    options
                        .UseMySQL(configuration.ConnectionString);
                });

            return services;
        }
    }
}