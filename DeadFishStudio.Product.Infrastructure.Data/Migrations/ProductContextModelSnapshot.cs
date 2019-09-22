﻿// <auto-generated />
using System;
using DeadFishStudio.Product.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DeadFishStudio.Product.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ProductContext))]
    partial class ProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("deadfish")
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DeadFishStudio.Product.Domain.Model.Entity.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PDCT_SQ_PRODUCT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("PDCT_NAME");

                    b.Property<int>("Quantity")
                        .HasColumnName("PDCT_QUANTITY");

                    b.HasKey("Id");

                    b.ToTable("PRODUCT");
                });

            modelBuilder.Entity("DeadFishStudio.Product.Domain.Model.Entity.Product", b =>
                {
                    b.OwnsMany("DeadFishStudio.Product.Domain.Model.ObjectOfValue.Price", "Prices", b1 =>
                        {
                            b1.Property<bool>("IsActive")
                                .HasColumnName("PRCE_IN_ACTIVE");

                            b1.Property<DateTime>("CreateDate")
                                .ValueGeneratedOnAdd()
                                .HasColumnName("PRCE_DT_CREATED");

                            b1.Property<decimal>("Amount")
                                .HasColumnName("PRCE_AMOUNT");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnName("PRCE_CURRENCY");

                            b1.Property<Guid>("PDCT_SQ_PRODUCT");

                            b1.HasKey("IsActive", "CreateDate");

                            b1.HasIndex("PDCT_SQ_PRODUCT");

                            b1.ToTable("PRICE");

                            b1.HasOne("DeadFishStudio.Product.Domain.Model.Entity.Product")
                                .WithMany("Prices")
                                .HasForeignKey("PDCT_SQ_PRODUCT")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
