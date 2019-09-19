using System;
using System.Collections.Generic;
using System.Linq;
using GianLuca.Domain.Core.Entity;

namespace DeadFishStudio.MarketList.Domain.Model.Entities
{
    public class MarketList<T> : BaseEntity where T : BaseEntity
    {
        private string _name = string.Empty;
        public string Nome
        {
            get => _name;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _name = value;
            }
        }

        private DateTime? _dataDeCriacao;
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

        private DateTime? _dataDeModificacao;
        public DateTime DataDeModificacao
        {
            get => _dataDeModificacao ?? DateTime.Now;
            set => _dataDeModificacao = DateTime.Now;
        }

        public List<Items<T>> Items { get; } = new List<Items<T>>();

        public bool AdicionaItemALista(Items<T> items)
        {
            if (items is null)
            {
                AddNotification("items", "O items a ser inserido não pode ser nulo.");
                return false;
            }

            if (items.Count <= 0)
            {
                AddNotification("Items", "A lista deve conter items para que o possa ser removido.");
                return false;
            }

            if (Items is null)
            {
                AddNotification("Items", "A lista de Items não pode ser nula.");
                return false;
            }

            if (Items.Contains(items))
            {
                AddNotification("items", "Items a ser inserido já esta contido na lista");
                return false;
            }

            Items.Add(items);

            return Items.Count > 0;
        }

        public bool RemoveItemDaLista(Items<T> items)
        {
            if (items is null)
            {
                AddNotification("items", "O item a ser removido não pode ser nulo.");
                return false;
            }

            if (items.Count <= 0)
            {
                AddNotification("Items", "A lista deve conter items para que o possa ser removido.");
                return false;
            }

            if (Items is null)
            {
                AddNotification("Items", "A lista de Items não pode ser nula.");
                return false;
            }

            if (Items.Count <= 0)
            {
                AddNotification("Items", "A lista deve conter items para que o possa ser removido.");
                return false;
            }

            if (!Items.Contains(items))
            {
                AddNotification("Items", "A lista não contém o elemento.");
                return false;
            }

            Items.Remove(items);

            return !Items.Contains(items);
        }
    }
}