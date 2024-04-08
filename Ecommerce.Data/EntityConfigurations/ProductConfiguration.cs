using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Ecommerce.Data.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id);
            builder.HasOne(e => e.Category).WithMany(e => e.Products).HasForeignKey(e=>e.CategoryId);
            builder.Property(e => e.Name).IsRequired().HasColumnName("Product Name");
            builder.Property(e => e.Description).IsRequired().HasColumnName("Product Description");
        }
    }
}
