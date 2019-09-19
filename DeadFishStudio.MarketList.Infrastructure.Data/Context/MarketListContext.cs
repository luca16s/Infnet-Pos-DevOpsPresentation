using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using DeadFishStudio.MarketList.Infrastructure.Data.Context.Configurations;
using GianLuca.Domain.Core.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DeadFishStudio.MarketList.Infrastructure.Data.Context
{
    public class MarketListContext : DbContext, IUnitOfWork
    {
        public const string DefaultSchema = "deadfish";

        private IDbContextTransaction _currentTransaction;

        public MarketListContext(DbContextOptions options) : base(options)
        {
        }

        public bool HasActiveTransaction => _currentTransaction != null;

        public virtual DbSet<Domain.Model.Entities.MarketList> MarketListDbSet { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _ = builder.HasDefaultSchema(DefaultSchema);
            _ = builder.ApplyConfiguration(new MarketListConfiguration());
            _ = builder.ApplyConfiguration(new ItemsConfiguration());
            _ = builder.ApplyConfiguration(new ProductConfiguration());
            _ = builder.ApplyConfiguration(new PriceConfiguration());
            base.OnModelCreating(builder);
        }
    }
}