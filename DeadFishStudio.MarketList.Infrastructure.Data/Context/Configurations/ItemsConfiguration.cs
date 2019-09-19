using DeadFishStudio.MarketList.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeadFishStudio.MarketList.Infrastructure.Data.Context.Configurations
{
    public class ItemsConfiguration : IEntityTypeConfiguration<Items<Product.Domain.Model.Entity.Product>>
    {
        public void Configure(EntityTypeBuilder<Items<Product.Domain.Model.Entity.Product>> builder)
        {
            builder.ToTable("items");
        }
    }
}