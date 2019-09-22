using System;
using GianLuca.Domain.Core.Entity;

namespace DeadFishStudio.MarketList.Domain.Model.Entities
{
    public class MarketList : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketList"/> class.
        /// Constructor for EFCore.
        /// </summary>
        public MarketList()
        {
        }

        private DateTime? _dataDeCriacao;
        private DateTime? _dataDeModificacao;
        private string _name = string.Empty;

        public string Name
        {
            get => _name;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _name = value;
            }
        }

        public DateTime DataDeCriacao
        {
            get => _dataDeCriacao ?? DateTime.Now;
            set
            {
                if (_dataDeCriacao is null
                    && value != new DateTime())
                    _dataDeCriacao = value;
            }
        }

        public DateTime DataDeModificacao
        {
            get => _dataDeModificacao ?? DateTime.Now;
            set => _dataDeModificacao = DateTime.Now;
        }

        public Items<MarketListProduct> Items { get; } = new Items<MarketListProduct>();

        public bool AdicionaItemALista(MarketListProduct item)
        {
            if (item is null)
            {
                AddNotification("item", "O item a ser inserido não pode ser nulo.");
                return false;
            }

            if (Items is null)
            {
                AddNotification("ItemsId", "A lista de ItemsId não pode ser nula.");
                return false;
            }

            if (Items.Contains(item))
            {
                AddNotification("item", "ItemsId a ser inserido já esta contido na lista");
                return false;
            }

            Items.Add(item);

            return Items.Count > 0;
        }

        public bool RemoveItemDaLista(MarketListProduct item)
        {
            if (item is null)
            {
                AddNotification("item", "O item a ser removido nao pode ser nulo.");
                return false;
            }

            if (Items is null)
            {
                AddNotification("ItemsId", "A lista de ItemsId nao pode ser nula.");
                return false;
            }

            if (Items.Count <= 0)
            {
                AddNotification("ItemsId", "A lista deve conter item para que o possa ser removido.");
                return false;
            }

            if (!Items.Contains(item))
            {
                AddNotification("ItemsId", "A lista nao contém o elemento.");
                return false;
            }

            Items.Remove(item);

            return !Items.Contains(item);
        }
    }
}