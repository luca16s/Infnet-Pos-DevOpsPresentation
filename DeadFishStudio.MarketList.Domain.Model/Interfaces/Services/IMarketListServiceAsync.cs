using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeadFishStudio.MarketList.Domain.Model.Entities;
using GianLuca.Domain.Core.Entity;
using GianLuca.Domain.Core.Interfaces.Services;

namespace DeadFishStudio.MarketList.Domain.Model.Interfaces.Services
{
    public interface IMarketListServiceAsync<T> : IBaseServiceAsync<MarketList<T>> where T: BaseEntity
    {
        int CalculaQuantidadeDeItemsNaLista(List<Items<T>> items);
        decimal CalculaPrecoTotalDaLista(List<Items<T>> items);
        int CalculaQuantidadeTotalPorItem(string nomeItem, List<Items<T>> items);
        decimal CalculaPrecoTotalPorItem(string nomeItem, List<Items<T>> items);
    }
}