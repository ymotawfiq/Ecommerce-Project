

using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.ParentCategoryId).HasColumnName("Parent Category Id");
            builder.Property(e => e.CategoryName).IsRequired().HasColumnName("Category Name");
        }
    }
}
