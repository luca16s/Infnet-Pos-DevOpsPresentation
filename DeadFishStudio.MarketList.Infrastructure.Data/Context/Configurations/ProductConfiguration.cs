using DeadFishStudio.Product.Domain.Model.ObjectOfValue;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeadFishStudio.MarketList.Infrastructure.Data.Context.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product.Domain.Model.Entity.Product>
    {
        public void Configure(EntityTypeBuilder<Product.Domain.Model.Entity.Product> builder)
        {
            builder
                .ToTable("PRODUCT");

            builder.HasKey(product => product.Id);

            builder
                .Property(product => product.Id)
                .HasColumnName("PDCT_SQ_PRODUCT");

            builder
                .Property(product => product.Name)
                .HasColumnName("PDCT_NAME");

            builder
                .Property(product => product.Quantity)
                .HasColumnName("PDCT_QUANTITY");

            builder
                .OwnsMany<Price>("Prices", price =>
                {
                    price.HasForeignKey("PDCT_SQ_PRODUCT");
                    price.HasKey("PDCT_SQ_PRODUCT");
                });

            builder
                .Ignore(product => product.Notifications);
        }
    }
}