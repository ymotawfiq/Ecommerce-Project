

using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class ProductVariationConfiguration : IEntityTypeConfiguration<ProductVariation>
    {
        public void Configure(EntityTypeBuilder<ProductVariation> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.ProductItem).WithMany(e => e.ProductVariation2)
                .HasForeignKey(e => e.ProductItemId).IsRequired(false);
            builder.HasOne(e => e.VariationOption).WithMany(e => e.ProductVariation1)
                .HasForeignKey(e => e.VariationOptionId).IsRequired(false);
        }
    }
}
