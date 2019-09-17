using DeadFishStudio.Product.Infrastructure.Data.Context.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DeadFishStudio.Product.Infrastructure.Data.Context
{
    public class ProductContext: DbContext
    {
        public const string DefaultSchema = "deadfish";

        public virtual DbSet<Domain.Model.Entity.Product> ProductDbSet { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            _ = builder.HasDefaultSchema(DefaultSchema);
            _ = builder.ApplyConfiguration(new ProductConfiguration());
            _ = builder.ApplyConfiguration(new PriceConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
