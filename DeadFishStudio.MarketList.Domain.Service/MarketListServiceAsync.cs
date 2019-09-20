using System;
using System.Linq;
using DeadFishStudio.MarketList.Domain.Model;
using DeadFishStudio.MarketList.Domain.Model.Entities;
using DeadFishStudio.MarketList.Domain.Model.Interfaces.Repositories;
using DeadFishStudio.MarketList.Domain.Model.Interfaces.Services;

namespace DeadFishStudio.MarketList.Domain.Service
{
    public class MarketListServiceAsync : BaseServiceAsync<Model.Entities.MarketList>,
        IMarketListServiceAsync
    {
        public MarketListServiceAsync(IMarketListRepositoryAsync marketListRepositoryAsync) : base(
            marketListRepositoryAsync)
        {
        }

        public MarketListServiceAsync() : base()
        {
        }

        public int CalculaQuantidadeDeItemsNaLista(Items<MarketListProduct> items)
        {
            if (items == null) return 0;

            if (items.Count < 0) throw new InvalidOperationException();

            var quantidadeTotal = 0;

            items.ToList().ForEach(y => quantidadeTotal += y.Quantity);

            return quantidadeTotal;
        }

        public decimal CalculaPrecoTotalDaLista(Items<MarketListProduct> items)
        {
            if (items == null) return 0;

            if (items.Count < 0) throw new InvalidOperationException();

            decimal valorTotal = default;

            items.ToList().ForEach(y => valorTotal = y.Price * y.Quantity);

            return valorTotal;
        }

        public int CalculaQuantidadeTotalPorItem(string nomeItem,
            Items<MarketListProduct> items)
        {
            if (items == null) return 0;

            if (items.Count < 0) throw new InvalidOperationException();

            if (items.Count == 0) return 0;

            ILookup<string, int> dicItemQuantidade = null;
            dicItemQuantidade = items.ToList().ToLookup(y => y.Name, y => y.Quantity);

            return dicItemQuantidade[nomeItem].Sum();
        }

        public decimal CalculaPrecoTotalPorItem(string nomeItem, Items<MarketListProduct> items)
        {
            if (items == null) return 0;

            if (items.Count < 0) throw new InvalidOperationException();

            if (items.Count == 0) return 0;

            ILookup<string, decimal> dicItemPreco = null;
            dicItemPreco = items.ToList().ToLookup(y => y.Name, y => y.Price);

            return dicItemPreco[nomeItem].Sum();
        }
    }
}