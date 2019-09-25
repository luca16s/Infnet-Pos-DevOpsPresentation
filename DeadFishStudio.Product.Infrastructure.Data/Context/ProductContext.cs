using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using DeadFishStudio.Product.Infrastructure.Data.Context.Configuration;
using GianLuca.Domain.Core.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DeadFishStudio.Product.Infrastructure.Data.Context
{
    public sealed class ProductContext : DbContext, IUnitOfWork
    {
        public const string DefaultSchema = "deadfish";
        private IDbContextTransaction _currentTransaction;

        public DbSet<Domain.Model.Entity.Product> Product { get; set; }
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        public ProductContext(DbContextOptions options) : base(options)
        {
        }

        public ProductContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _ = builder.HasDefaultSchema(DefaultSchema);
            _ = builder.ApplyConfiguration(new ProductConfiguration());
            _ = builder.ApplyConfiguration(new PriceConfiguration());
            base.OnModelCreating(builder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            return (await SaveChangesAsync(cancellationToken)) > 0;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transação {transaction.TransactionId} não é a atual.");

            try
            {
                var isObjectSavedAsync = await SaveEntitiesAsync();

                if (isObjectSavedAsync)
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