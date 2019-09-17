﻿using DeadFishStudio.Product.Infrastructure.Data.Context;
using GianLuca.Domain.Core.Interfaces.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeadFishStudio.Product.Infrastructure.CrossCutting
{
    public static class Injection
    {
        public static void Services(IServiceCollection services, IConfiguration configuration)
        {
            var productConfig = new ProductConfiguration();
            configuration.GetSection(nameof(ProductConfiguration)).Bind(productConfig);

            //services.AddTransient(typeof(IProductRepositoryAsync), typeof(ProductRepositoryAsync));
            //services.AddTransient(typeof(IProductServiceAsync), typeof(ProductServiceAsync));
            services.AddTransient(typeof(IUnitOfWork), typeof(ProductContext));
            services.AddCustomDbContext(productConfig);
        }
    }
}