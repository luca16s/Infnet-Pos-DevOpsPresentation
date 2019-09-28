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
    public sealed class MarketListContext : DbContext, IUnitOfWork
    {
        public const string DefaultSchema = "deadfish";
        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        public MarketListContext()
        {
            Database?.EnsureCreated();
        }

        public MarketListContext(DbContextOptions options) : base(options)
        {
            Database?.EnsureCreated();
        }

        public DbSet<Domain.Model.Entities.MarketList> MarketList { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _ = builder.HasDefaultSchema(DefaultSchema);
            _ = builder.ApplyConfiguration(new MarketListConfiguration());
            base.OnModelCreating(builder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            await Database.EnsureCreatedAsync();

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transação {transaction.TransactionId} não é a atual.");

            try
            {
                var isObjectSaved = await SaveEntitiesAsync();

                if (isObjectSaved)
                    transaction.Commit();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}