using DeadFishStudio.MarketList.Domain.Model.Interfaces.Repositories;
using DeadFishStudio.MarketList.Infrastructure.Data.Context;

namespace DeadFishStudio.MarketList.Infrastructure.Data.Repositories
{
    public class MarketListRepositoryAsync : BaseRepositoryAsync<Domain.Model.Entities.MarketList>,
        IMarketListRepositoryAsync
    {
        public MarketListRepositoryAsync(MarketListContext marketListContext) : base(marketListContext)
        {
        }
    }
}