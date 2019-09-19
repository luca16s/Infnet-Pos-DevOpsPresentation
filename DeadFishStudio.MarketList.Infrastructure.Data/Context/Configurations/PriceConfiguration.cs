using DeadFishStudio.Product.Domain.Model.ObjectOfValue;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeadFishStudio.MarketList.Infrastructure.Data.Context.Configurations
{
    public class PriceConfiguration : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder
                .ToTable("PRICE");

            builder
                .Property(price => price.Currency)
                .HasColumnName("PRCE_CURRENCY");

            builder
                .Property(price => price.Amount)
                .HasColumnName("PRCE_AMOUNT");
        }
    }
}