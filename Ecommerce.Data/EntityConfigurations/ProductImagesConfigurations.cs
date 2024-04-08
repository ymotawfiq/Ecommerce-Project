

using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class ProductImagesConfigurations : IEntityTypeConfiguration<ProductImages>
    {
        public void Configure(EntityTypeBuilder<ProductImages> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Product).WithMany(e => e.ProductImages).HasForeignKey(e => e.ProductId);
            builder.Property(e => e.ProductImageUrl).IsRequired();
        }
    }
}
