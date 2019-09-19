using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GianLuca.Domain.Core.Entity;
using GianLuca.Domain.Core.Interfaces.Repository;

namespace DeadFishStudio.MarketList.Domain.Service
{
    public class BaseServiceAsync<T> : IBaseServiceAsync<T> where T : BaseEntity
    {
        private readonly IBaseRepositoryAsync<T> _repositoryAsync;

        public BaseServiceAsync()
        {
        }

        public BaseServiceAsync(IBaseRepositoryAsync<T> baseRepositoryAsync)
        {
            _repositoryAsync = baseRepositoryAsync;
        }

        public async Task<T> AddItemAsync(T item)
        {
            return await _repositoryAsync.AddItemAsync(item);
        }

        public void DeleteItem(T item)
        {
            _repositoryAsync.DeleteItem(item);
        }

        public async Task<IEnumerable<T>> GetAllItemsAsync()
        {
            return await _repositoryAsync.GetAllItemsAsync();
        }

        public async Task<T> GetItemAsync(Guid id)
        {
            return await _repositoryAsync.GetItemAsync(id);
        }

        public T UpdateItem(Guid id, T item)
        {
            return _repositoryAsync.UpdateItem(id, item);
        }
    }

    public interface IBaseServiceAsync<T> where T : BaseEntity
    {
    }
}