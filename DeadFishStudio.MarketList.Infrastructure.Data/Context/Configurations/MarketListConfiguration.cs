using DeadFishStudio.MarketList.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeadFishStudio.MarketList.Infrastructure.Data.Context.Configurations
{
    public class MarketListConfiguration : IEntityTypeConfiguration<Domain.Model.Entities.MarketList>
    {
        public void Configure(EntityTypeBuilder<Domain.Model.Entities.MarketList> builder)
        {
            builder
                .ToTable("marketlist");

            builder
                .HasKey(ml => ml.Id);

            builder
                .Property(ml => ml.Id)
                .IsRequired()
                .HasColumnName("MKLT_SQ_MARKET_LIST");

            builder
                .Property(ml => ml.Nome)
                .IsRequired()
                .HasColumnName("MKLT_NM_MARKET_LIST");

            builder
                .Property(ml => ml.DataDeCriacao)
                .IsRequired()
                .HasColumnName("MKLT_DT_MARKET_LIST");

            builder
                .OwnsMany<Items<Product.Domain.Model.Entity.Product>>("Items", it =>
                {
                    it.HasForeignKey("MKLT_SQ_MARKET_LIST");
                    it.HasKey("MKLT_SQ_MARKET_LIST");
                });

            builder
                .Ignore(ml => ml.Notifications);
        }
    }
}