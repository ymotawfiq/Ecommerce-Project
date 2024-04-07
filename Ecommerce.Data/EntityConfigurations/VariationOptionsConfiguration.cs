

using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class VariationOptionsConfiguration : IEntityTypeConfiguration<VariationOptions>
    {
        public void Configure(EntityTypeBuilder<VariationOptions> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Variation).WithMany(e => e.VariationOptions).HasForeignKey(e => e.VariationId);
            builder.Property(e => e.Value).IsRequired().HasColumnName("Variation Name");
        }
    }
}
