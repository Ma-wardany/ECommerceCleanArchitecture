using E_COMMERCE_APP.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Infrastructure.Context.Configs
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);


            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .HasColumnType("VARCHAR")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(300)
                .HasColumnType("NVARCHAR")
                .IsRequired();

            builder.Property(p => p.Price)
                .HasPrecision(15, 2)
                .IsRequired();

            builder.Property(p => p.Quantity).IsRequired();

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();

            builder.HasMany(p => p.Carts)
                .WithOne(c => c.Product)
                .HasForeignKey(p => p.ProductId)
                .IsRequired();

            builder.HasMany(p => p.Reviews)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId)
                .IsRequired();

            builder.HasMany(p => p.Images)
                .WithOne(i => i.product)
                .HasForeignKey(i => i.ProductId)
                .IsRequired();
        }
    }
}
