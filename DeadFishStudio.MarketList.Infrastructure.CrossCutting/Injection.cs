using DeadFishStudio.MarketList.Domain.Model.Interfaces.Repositories;
using DeadFishStudio.MarketList.Domain.Model.Interfaces.Services;
using DeadFishStudio.MarketList.Domain.Service;
using DeadFishStudio.MarketList.Infrastructure.Data.Context;
using DeadFishStudio.MarketList.Infrastructure.Data.Repositories;
using GianLuca.Domain.Core.Interfaces.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeadFishStudio.MarketList.Infrastructure.CrossCutting
{
    public static class Injection
    {
        public static void Services(IServiceCollection services, IConfiguration configuration)
        {
            var marketListConfig = new MarketListConfiguration();
            configuration.GetSection(nameof(MarketListConfiguration)).Bind(marketListConfig);

            services.AddScoped(typeof(IMarketListRepositoryAsync), typeof(MarketListRepositoryAsync));
            services.AddScoped(typeof(IMarketListServiceAsync), typeof(MarketListServiceAsync));
            services.AddScoped(typeof(IUnitOfWork), typeof(MarketListContext));
            services.AddCustomDbContext(marketListConfig);
        }
    }
}