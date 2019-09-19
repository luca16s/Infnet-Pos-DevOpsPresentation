using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeadFishStudio.MarketList.Domain.Model.Entities;
using DeadFishStudio.MarketList.Domain.Model.Interfaces.Services;
using GianLuca.Domain.Core.Interfaces.Repository;

namespace DeadFishStudio.MarketList.Domain.Service
{
    public class MarketListServiceAsync : BaseServiceAsync<MarketList<Product.Domain.Model.Entity.Product>>, IMarketListServiceAsync<Product.Domain.Model.Entity.Product>
    {
        public MarketListServiceAsync()
        {
        }

        public MarketListServiceAsync(IBaseRepositoryAsync<MarketList<Product.Domain.Model.Entity.Product>> baseRepositoryAsync) : base(baseRepositoryAsync)
        {
        }

        public int CalculaQuantidadeDeItemsNaLista(List<Items<Product.Domain.Model.Entity.Product>> items)
        {
            if (items == null)
            {
                return 0;
            }

            if (items.Count < 0)
            {
                throw new InvalidOperationException();
            }

            var quantidadeTotal = 0;

            items.ForEach(x => x.ToList().ForEach(y => quantidadeTotal += y.Quantity));

            return quantidadeTotal;
        }

        public decimal CalculaPrecoTotalDaLista(List<Items<Product.Domain.Model.Entity.Product>> items)
        {
            if (items == null)
            {
                return 0;
            }

            if (items.Count < 0)
            {
                throw new InvalidOperationException();
            }

            decimal valorTotal = default;

            items.ToList().ForEach(x => x.ToList().ForEach(y => valorTotal = y.Prices[0].Amount * y.Quantity));

            return valorTotal;
        }

        public int CalculaQuantidadeTotalPorItem(string nomeItem, List<Items<Product.Domain.Model.Entity.Product>> items)
        {
            if (items == null)
            {
                return 0;
            }

            if (items.Count < 0)
            {
                throw new InvalidOperationException();
            }

            if (items.Count == 0)
            {
                return 0;
            }

            ILookup<string, int> dicItemQuantidade = null;
            items.ToList().ForEach(x => dicItemQuantidade = x.ToList().ToLookup(y => y.Name, y => y.Quantity));

            return dicItemQuantidade[nomeItem].Sum();
        }

        public decimal CalculaPrecoTotalPorItem(string nomeItem, List<Items<Product.Domain.Model.Entity.Product>> items)
        {
            if (items == null)
            {
                return 0;
            }

            if (items.Count < 0)
            {
                throw new InvalidOperationException();
            }

            if (items.Count == 0)
            {
                return 0;
            }

            ILookup<string, decimal> dicItemPreco = null;
            items.ToList().ForEach(x => dicItemPreco = x.ToList().ToLookup(y => y.Name, y => y.Prices[0].Amount));

            return dicItemPreco[nomeItem].Sum();
        }
    }
}
