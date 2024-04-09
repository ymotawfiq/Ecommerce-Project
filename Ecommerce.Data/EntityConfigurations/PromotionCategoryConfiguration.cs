
using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class PromotionCategoryConfiguration : IEntityTypeConfiguration<PromotionCategory>
    {
        public void Configure(EntityTypeBuilder<PromotionCategory> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.ProductCategory).WithMany(e => e.PromotionCategories)
                .HasForeignKey(e => e.CategoryId).IsRequired(false);
            builder.HasOne(e => e.Promotion).WithMany(e => e.PromotionCategories).HasForeignKey(e => e.PromotionId);
        }
    }
}
