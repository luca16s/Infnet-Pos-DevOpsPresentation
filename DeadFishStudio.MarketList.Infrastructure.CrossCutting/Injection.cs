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

            services.AddTransient(typeof(IMarketListRepositoryAsync), typeof(MarketListRepositoryAsync));
            services.AddTransient(typeof(IMarketListServiceAsync), typeof(MarketListServiceAsync));
            services.AddTransient(typeof(IUnitOfWork), typeof(MarketListContext));
            services.AddCustomDbContext(marketListConfig);
        }
    }
}