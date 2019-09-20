using DeadFishStudio.MarketList.Domain.Model.Entities;
using GianLuca.Domain.Core.Interfaces.Services;

namespace DeadFishStudio.MarketList.Domain.Model.Interfaces.Services
{
    public interface IMarketListServiceAsync : IBaseServiceAsync<Entities.MarketList>
    {
        int CalculaQuantidadeDeItemsNaLista(Items<MarketListProduct> items);
        decimal CalculaPrecoTotalDaLista(Items<MarketListProduct> items);
        int CalculaQuantidadeTotalPorItem(string nomeItem, Items<MarketListProduct> items);
        decimal CalculaPrecoTotalPorItem(string nomeItem, Items<MarketListProduct> items);
    }
}