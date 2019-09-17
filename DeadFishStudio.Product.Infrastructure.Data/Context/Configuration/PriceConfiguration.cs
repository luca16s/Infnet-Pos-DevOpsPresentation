using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeadFishStudio.Product.Infrastructure.Data.Context.Configuration
{
    public class PriceConfiguration : IEntityTypeConfiguration<Domain.Model.ObjectOfValue.Price>
    {
        public void Configure(EntityTypeBuilder<Domain.Model.ObjectOfValue.Price> builder)
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
