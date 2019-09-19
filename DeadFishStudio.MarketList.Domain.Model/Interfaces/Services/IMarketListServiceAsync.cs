using System.Collections.Generic;
using DeadFishStudio.MarketList.Domain.Model.Entities;
using GianLuca.Domain.Core.Entity;
using GianLuca.Domain.Core.Interfaces.Services;

namespace DeadFishStudio.MarketList.Domain.Model.Interfaces.Services
{
    public interface IMarketListServiceAsync : IBaseServiceAsync<Entities.MarketList>
    {
        int CalculaQuantidadeDeItemsNaLista(Items<Product.Domain.Model.Entity.Product> items);
        decimal CalculaPrecoTotalDaLista(Items<Product.Domain.Model.Entity.Product> items);
        int CalculaQuantidadeTotalPorItem(string nomeItem, Items<Product.Domain.Model.Entity.Product> items);
        decimal CalculaPrecoTotalPorItem(string nomeItem, Items<Product.Domain.Model.Entity.Product> items);
    }
}