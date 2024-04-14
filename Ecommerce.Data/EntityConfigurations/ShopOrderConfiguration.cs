

using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class ShopOrderConfiguration : IEntityTypeConfiguration<ShopOrder>
    {
        public void Configure(EntityTypeBuilder<ShopOrder> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.OrderStatus).WithMany(e => e.ShopOrders).HasForeignKey(e => e.OrderStatusId);
            builder.HasOne(e => e.Address).WithMany(e => e.ShopOrders).HasForeignKey(e => e.ShippingAddressId);
            builder.HasOne(e => e.PaymentMethod).WithMany(e => e.ShopOrders)
                .HasForeignKey(e => e.PaymentMethodId).IsRequired(false);
            builder.HasOne(e => e.ShippingMethod).WithMany(e => e.ShopOrders).HasForeignKey(e => e.ShippingMethodId);
            builder.HasOne(e => e.User).WithMany(e => e.ShopOrders).HasForeignKey(e => e.UserId);
            builder.Property(e => e.OrderDate).IsRequired().HasColumnName("Order Date");
            builder.Property(e => e.OrderTotal).IsRequired().HasColumnName("Total Order Price");
        }
    }
}
