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
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(c => new {c.UserId, c.ProductId});

            builder.Property(c => c.Quantity)
                .IsRequired();

            builder.Property(c => c.Date)
                .IsRequired();

            builder.HasOne(c => c.applicationUser)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .IsRequired();
        }
    }
}
