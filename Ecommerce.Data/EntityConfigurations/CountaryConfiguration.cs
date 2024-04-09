

using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class CountaryConfiguration : IEntityTypeConfiguration<Countary>
    {
        public void Configure(EntityTypeBuilder<Countary> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasColumnName("Countary Name");
        }
    }
}
