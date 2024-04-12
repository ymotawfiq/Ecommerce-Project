

using Ecommerce.Data.Models.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class SiteUserConfiguration : IEntityTypeConfiguration<SiteUser>
    {
        public void Configure(EntityTypeBuilder<SiteUser> builder)
        {
            builder.Property(e => e.FirstName).IsRequired().HasColumnName("First Name");
            builder.Property(e => e.LastName).IsRequired().HasColumnName("Last Name");
        }
    }
}
