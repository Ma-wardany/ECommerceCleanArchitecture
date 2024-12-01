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
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("Order Products");

            builder.HasKey(op => new { op.OrderId, op.ProductId });

            builder.HasOne(op => op.order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId)
                .IsRequired();

            builder.HasOne(op => op.product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId)
                .IsRequired();
        }
    }
}
