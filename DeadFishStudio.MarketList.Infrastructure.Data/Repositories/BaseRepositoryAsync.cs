using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeadFishStudio.MarketList.Infrastructure.Data.Context;
using GianLuca.Domain.Core.Entity;
using GianLuca.Domain.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace DeadFishStudio.MarketList.Infrastructure.Data.Repositories
{
    public class BaseRepositoryAsync<T> : IBaseRepositoryAsync<T> where T : BaseEntity
    {
        protected MarketListContext Context;

        public BaseRepositoryAsync(MarketListContext marketListContext)
        {
            Context = marketListContext;
            DbSet = Context.Set<T>();
        }

        protected DbSet<T> DbSet { get; }

        public async Task<T> AddItemAsync(T item)
        {
            try
            {
                var entity = await DbSet.AddAsync(item);
                entity.State = EntityState.Added;
                await Context.SaveEntitiesAsync();
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
            DbSet.Local.Add(item);
            var entry = DbSet.Update(item);
            entry.State = EntityState.Modified;
            Context.SaveChanges();
            return entry.Entity;
        }

        public void DeleteItem(T item)
        {
            DbSet.Remove(item);
            Context.SaveChanges();
        }
    }
}