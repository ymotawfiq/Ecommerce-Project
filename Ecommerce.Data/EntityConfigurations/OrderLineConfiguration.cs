

using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.ProductItem).WithMany(e => e.OrderLines).HasForeignKey(e => e.ProductItemId);
            builder.HasOne(e => e.ShopOrder).WithMany(e => e.OrderLines).HasForeignKey(e => e.ShopOrderId);
            builder.Property(e => e.Qty).IsRequired().HasColumnName("Order quantity");
            builder.Property(e => e.Price).IsRequired().HasColumnName("Order Price");
        }
    }
}
