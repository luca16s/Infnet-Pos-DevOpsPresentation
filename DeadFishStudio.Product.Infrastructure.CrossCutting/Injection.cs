using DeadFishStudio.Product.Domain.Model.Interfaces.Repositories;
using DeadFishStudio.Product.Domain.Model.Interfaces.Services;
using DeadFishStudio.Product.Domain.Service;
using DeadFishStudio.Product.Infrastructure.Data.Context;
using DeadFishStudio.Product.Infrastructure.Data.Repositories;
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

            services.AddSingleton(typeof(IProductRepositoryAsync), typeof(ProductRepositoryAsync));
            services.AddSingleton(typeof(IProductServiceAsync), typeof(ProductServiceAsync));
            services.AddSingleton(typeof(IUnitOfWork), typeof(ProductContext));
            services.AddCustomDbContext(productConfig);
        }
    }
}