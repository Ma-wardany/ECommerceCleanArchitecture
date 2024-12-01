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

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Price)
                .HasPrecision(15, 2);

            builder.Property(o => o.Status)
                .IsRequired();

            builder.Property(o => o.OrderNumber)
                .HasMaxLength(50)
                .HasColumnType("VARCHAR")
                .IsRequired();

            builder.HasOne(o => o.applicationUser)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .IsRequired();
          
        }
    }
}
