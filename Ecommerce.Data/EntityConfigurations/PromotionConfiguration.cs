

using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasColumnName("Promotion Name");
            builder.Property(e => e.StartDate).IsRequired().HasColumnName("Promotion Starting Date");
            builder.Property(e => e.Description).IsRequired().HasColumnName("Promotion Description");
            builder.Property(e => e.EndDate).IsRequired().HasColumnName("Promotion Ending Date");
        }
    }
}
