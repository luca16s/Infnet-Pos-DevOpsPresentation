using System.Threading;
using System.Threading.Tasks;
using DeadFishStudio.Product.Infrastructure.Data.Context.Configuration;
using GianLuca.Domain.Core.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DeadFishStudio.Product.Infrastructure.Data.Context
{
    public class ProductContext: DbContext, IUnitOfWork
    {
        public const string DefaultSchema = "deadfish";

        public ProductContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Domain.Model.Entity.Product> ProductDbSet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _ = builder.HasDefaultSchema(DefaultSchema);
            _ = builder.ApplyConfiguration(new ProductConfiguration());
            _ = builder.ApplyConfiguration(new PriceConfiguration());
            base.OnModelCreating(builder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            throw new System.NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new System.NotImplementedException();
        }
    }
}
