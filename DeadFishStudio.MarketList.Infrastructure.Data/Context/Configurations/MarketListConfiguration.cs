using System;
using System.Collections.Generic;
using DeadFishStudio.MarketList.Domain.Model;
using DeadFishStudio.MarketList.Domain.Model.Entities;
using GianLuca.Domain.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeadFishStudio.MarketList.Infrastructure.Data.Context.Configurations
{
    public class MarketListConfiguration : IEntityTypeConfiguration<Domain.Model.Entities.MarketList>
    {
        public void Configure(EntityTypeBuilder<Domain.Model.Entities.MarketList> builder)
        {
            builder
                .ToTable("MARKETLIST");

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
                .OwnsMany<MarketListProduct>("Items", it =>
                {
                    it.ToTable("ITEMS");
                    it.HasKey(x => x.ProductId);
                    it.Property(x => x.ProductId).HasColumnName("MKLT_SQ_PRODUCT");
                    it.Ignore(x => x.Price);
                    it.Ignore(x => x.Name);
                    it.Ignore(x => x.Quantity);
                    it.HasForeignKey("MKLT_SQ_MARKET_LIST");
                });

            builder
                .Ignore(ml => ml.Items);

            builder
                .Ignore(ml => ml.Notifications);
        }
    }
}