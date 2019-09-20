using System.Collections;
using System.Collections.Generic;

namespace DeadFishStudio.MarketList.Domain.Model.Entities
{
    public class Items<T> : IList<T>
    {
        private readonly IList<T> _listaItems = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return _listaItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _listaItems.Add(item);
        }

        public void Clear()
        {
            _listaItems.Clear();
        }

        public bool Contains(T item)
        {
            return _listaItems.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _listaItems.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _listaItems.Remove(item);
        }

        public int Count => _listaItems.Count;

        public bool IsReadOnly => _listaItems.IsReadOnly;

        public int IndexOf(T item)
        {
            return _listaItems.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _listaItems.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _listaItems.RemoveAt(index);
        }

        public T this[int index]
        {
            get => _listaItems[index];
            set => _listaItems[index] = value;
        }
    }
}