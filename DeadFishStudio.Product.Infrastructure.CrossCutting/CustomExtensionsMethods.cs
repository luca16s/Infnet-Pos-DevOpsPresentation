using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeadFishStudio.Product.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace DeadFishStudio.Product.Infrastructure.CrossCutting
{internal static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, ProductConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.AddEntityFrameworkMySQL()
                .AddDbContext<ProductContext>(options =>
                {
                    options
                        .UseMySQL(configuration.ConnectionString);
                });

            return services;
        }
    }
}