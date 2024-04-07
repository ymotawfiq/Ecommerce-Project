
using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class VariationConfigurations : IEntityTypeConfiguration<Variation>
    {
        public void Configure(EntityTypeBuilder<Variation> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Category).WithMany(e => e.Variations).HasForeignKey(e => e.CategoryId);
            builder.Property(e => e.Name).IsRequired().HasColumnName("Variation Title");
        }
    }
}
