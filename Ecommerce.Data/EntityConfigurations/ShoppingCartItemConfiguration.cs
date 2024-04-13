using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.EntityConfigurations
{
    public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.ProductItem).WithMany(e => e.ShoppingCartItems).HasForeignKey(e => e.ProductItemId);
            builder.HasOne(e => e.ShoppingCart).WithMany(e => e.ShoppingCartItems).HasForeignKey(e => e.CartId);
            builder.Property(e => e.Qty).IsRequired();
        }
    }
}
