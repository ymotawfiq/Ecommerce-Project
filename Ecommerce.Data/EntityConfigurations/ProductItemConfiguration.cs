

using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class ProductItemConfiguration : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Product).WithMany(e => e.ProductItems).HasForeignKey(e => e.ProductId);
            builder.Property(e => e.Price).IsRequired().HasColumnName("Product Item Price");
            builder.Property(e => e.ProducItemImageUrl).IsRequired().HasColumnName("Produc Item Image Url");
            builder.Property(e => e.QuantityInStock).IsRequired().HasColumnName("Quantity In Stock");
            builder.Property(e => e.SKU).IsRequired().HasColumnName("Stock keeping unit (SKU)");
        }
    }
}
