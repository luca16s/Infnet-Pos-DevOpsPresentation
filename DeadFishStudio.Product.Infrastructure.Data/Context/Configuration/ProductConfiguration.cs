﻿using DeadFishStudio.Product.Domain.Model.ObjectOfValue;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeadFishStudio.Product.Infrastructure.Data.Context.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Domain.Model.Entity.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Model.Entity.Product> builder)
        {
            builder
                .ToTable("PRODUCT");

            builder
                .HasKey(product => product.Id);

            builder
                .Property(product => product.Id)
                .HasColumnName("PDCT_SQ_PRODUCT")
                .IsRequired();

            builder
                .Property(product => product.Name)
                .HasColumnName("PDCT_NAME")
                .IsRequired();

            builder
                .Property(product => product.Quantity)
                .HasColumnName("PDCT_QUANTITY")
                .IsRequired();

            builder
                .OwnsMany<Price>("Prices", price =>
                {
                    price.HasForeignKey("PDCT_SQ_PRODUCT");
                    price.HasKey(p => new
                    {
                        p.IsActive,
                        p.CreateDate
                    });
                });

            builder
                .Ignore(product => product.Notifications);
        }
    }
}