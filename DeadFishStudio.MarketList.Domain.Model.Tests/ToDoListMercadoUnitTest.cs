using System;
using System.Collections.Generic;
using System.Linq;
using DeadFishStudio.MarketList.Domain.Model.Entities;
using DeadFishStudio.MarketList.Domain.Model.Interfaces.Services;
using DeadFishStudio.MarketList.Domain.Service;
using DeadFishStudio.Product.Domain.Model.ObjectOfValue;
using Xunit;

namespace DeadFishStudio.MarketList.Domain.Model.Tests
{
    public class ToDoListMercadoUnitTest
    {
        private readonly MarketList<Product.Domain.Model.Entity.Product> _listaMercado;
        private readonly Items<Product.Domain.Model.Entity.Product> _itemMercado1;
        private readonly Items<Product.Domain.Model.Entity.Product> _itemMercado2;
        private readonly MarketListServiceAsync _marketListService;

        public ToDoListMercadoUnitTest()
        {
            _listaMercado = new MarketList<Product.Domain.Model.Entity.Product>();
            _itemMercado1 = new Items<Product.Domain.Model.Entity.Product>();
            _itemMercado2 = new Items<Product.Domain.Model.Entity.Product>();
            _marketListService = new MarketListServiceAsync();

            var price1 = new Price()
            {
                Currency = "Real",
                Amount = 10m
            };

            var price2 = new Price()
            {
                Currency = "Real",
                Amount = 10m
            };

            _itemMercado1.Add(new Product.Domain.Model.Entity.Product()
            {
                Name = "Uva",
                Prices = new List<Price>()
                {
                    price1
                },
                Quantity = 10
            });

            _itemMercado2.Add(new Product.Domain.Model.Entity.Product()
            {
                Name = "Pera",
                Prices = new List<Price>()
                {
                    price2
                },
                Quantity = 15
            });
        }

        [Fact]
        public void AdicionaNomeNuloALista()
        {
            _listaMercado.Nome = null;

            Assert.Equal(string.Empty, _listaMercado.Nome);
        }

        [Fact]
        public void AdicionaNomeEmBrancoNaLista()
        {
            _listaMercado.Nome = string.Empty;

            Assert.Equal(string.Empty, _listaMercado.Nome);
        }

        [Fact]
        public void AdicionaNomeCorretoNaLista()
        {
            var nome = "Mercado";
            _listaMercado.Nome = nome;

            Assert.Equal(nome, _listaMercado.Nome);
        }

        [Fact]
        public void AdicionaDataPadrao()
        {
            var dataPadrao = new DateTime();
            _listaMercado.DataDeCriacao = dataPadrao;

            Assert.NotEqual(dataPadrao, _listaMercado.DataDeCriacao);
        }

        [Fact]
        public void AdicionaDataCorreta()
        {
            var dataAtual = DateTime.Now;
            _listaMercado.DataDeCriacao = dataAtual;

            Assert.Equal(_listaMercado.DataDeCriacao, dataAtual);
        }

        [Fact]
        public void AdicionaItemNaListaDeItemsNula()
        {
            _listaMercado.AdicionaItemALista(_itemMercado1);

            Assert.Contains(_itemMercado1, _listaMercado.Items);
        }

        [Fact]
        public void AdicionaItemNuloNaListaDeItems()
        {
            Items<Product.Domain.Model.Entity.Product> item = null;

            _listaMercado.AdicionaItemALista(item);

            Assert.DoesNotContain(item, _listaMercado.Items);
        }

        [Fact]
        public void AdicionaItemCorretoNaLista()
        {
            _listaMercado.AdicionaItemALista(_itemMercado1);

            Assert.Contains(_itemMercado1, _listaMercado.Items);
        }

        [Fact]
        public void RemoveItemDaListaNula()
        {
            _listaMercado.AdicionaItemALista(_itemMercado1);

            _listaMercado.RemoveItemDaLista(_itemMercado1);

            Assert.DoesNotContain(_itemMercado1, _listaMercado.Items);
        }

        [Fact]
        public void RemoveItemNuloDaLista()
        {
            Items<Product.Domain.Model.Entity.Product> item = null;

            _listaMercado.RemoveItemDaLista(item);

            VerificaMensagem("O item a ser removido não pode ser nulo.");
        }

        [Fact]
        public void RemoveItemDaLista()
        {
            _listaMercado.AdicionaItemALista(_itemMercado1);

            _listaMercado.RemoveItemDaLista(_itemMercado1);

            Assert.DoesNotContain(_itemMercado1, _listaMercado.Items);
        }

        [Fact]
        public void VerificaQuantidadeTotalDeItemsNaLista()
        {
            _listaMercado.AdicionaItemALista(_itemMercado1);
            _listaMercado.AdicionaItemALista(_itemMercado2);

            var total = _marketListService.CalculaQuantidadeDeItemsNaLista(_listaMercado.Items);

            Assert.Equal(25, total);
        }

        [Fact]
        public void VerificaPrecoTotalDaLista()
        {
            _listaMercado.AdicionaItemALista(_itemMercado1);

            //var resultado = _listaMercado.CalculaPrecoTotalDaLista(_listaMercado.Items);

            //Assert.Equal(50, resultado);
        }

        [Fact]
        public void VerificaQuantidadeDeUmProduto()
        {
            _listaMercado.AdicionaItemALista(_itemMercado1);
            _listaMercado.AdicionaItemALista(_itemMercado2);

            //_listaMercado.CalculaQuantidadeTotalPorItem(_itemMercado1.Nome, _listaMercado.Items);

            //Assert.Equal(15, _itemMercado2.Quantidade);
        }

        internal void VerificaMensagem(string message)
        {
            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            foreach (var notificacao in _listaMercado.Notifications)
            {
                Assert.Equal(message, notificacao.Message);
            }
        }
    }
}