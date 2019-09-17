using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeadFishStudio.Product.Infrastructure.Data.Context;
using GianLuca.Domain.Core.Entity;
using GianLuca.Domain.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace DeadFishStudio.Product.Infrastructure.Data.Repositories
{
    public class BaseRepositoryAsync<T> : IBaseRepositoryAsync<T> where T : BaseEntity
    {
        protected ProductContext Context;
        protected DbSet<T> DbSet { get; }

        public BaseRepositoryAsync(ProductContext productContext)
        {
            Context = productContext;
            DbSet = Context.Set<T>();
        }

        public async Task<T> AddItemAsync(T item)
        {
            try
            {
                var entity = await DbSet.AddAsync(item);
                entity.State = EntityState.Added;
                return entity.Entity;
            }
            catch (DbUpdateException e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<T> GetItemAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllItemsAsync()
        {
            return await DbSet.ToListAsync();
        }

        public T UpdateItem(Guid id, T item)
        {
            var entry = DbSet.Update(item);
            entry.State = EntityState.Modified;
            return entry.Entity;
        }

        public void DeleteItem(T item)
        {
            DbSet.Remove(item);
        }
    }
}