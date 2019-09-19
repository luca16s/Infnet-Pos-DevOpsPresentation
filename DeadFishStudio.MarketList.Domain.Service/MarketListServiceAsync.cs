using System;
using System.Collections.Generic;
using System.Linq;
using DeadFishStudio.MarketList.Domain.Model.Entities;
using DeadFishStudio.MarketList.Domain.Model.Interfaces.Repositories;
using DeadFishStudio.MarketList.Domain.Model.Interfaces.Services;
using GianLuca.Domain.Core.Interfaces.Repository;

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

        public int CalculaQuantidadeDeItemsNaLista(Items<Product.Domain.Model.Entity.Product> items)
        {
            if (items == null) return 0;

            if (items.Count < 0) throw new InvalidOperationException();

            var quantidadeTotal = 0;

            items.ToList().ForEach(y => quantidadeTotal += y.Quantity);

            return quantidadeTotal;
        }

        public decimal CalculaPrecoTotalDaLista(Items<Product.Domain.Model.Entity.Product> items)
        {
            if (items == null) return 0;

            if (items.Count < 0) throw new InvalidOperationException();

            decimal valorTotal = default;

            items.ToList().ForEach(y => valorTotal = y.Prices[0].Amount * y.Quantity);

            return valorTotal;
        }

        public int CalculaQuantidadeTotalPorItem(string nomeItem,
            Items<Product.Domain.Model.Entity.Product> items)
        {
            if (items == null) return 0;

            if (items.Count < 0) throw new InvalidOperationException();

            if (items.Count == 0) return 0;

            ILookup<string, int> dicItemQuantidade = null;
            dicItemQuantidade = items.ToList().ToLookup(y => y.Name, y => y.Quantity);

            return dicItemQuantidade[nomeItem].Sum();
        }

        public decimal CalculaPrecoTotalPorItem(string nomeItem, Items<Product.Domain.Model.Entity.Product> items)
        {
            if (items == null) return 0;

            if (items.Count < 0) throw new InvalidOperationException();

            if (items.Count == 0) return 0;

            ILookup<string, decimal> dicItemPreco = null;
            dicItemPreco = items.ToList().ToLookup(y => y.Name, y => y.Prices[0].Amount);

            return dicItemPreco[nomeItem].Sum();
        }
    }
}