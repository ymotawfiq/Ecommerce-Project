
using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Countary).WithMany(e => e.Addresses).HasForeignKey(e => e.CountaryId);
            builder.Property(e => e.AddressLine1).IsRequired();
            builder.Property(e => e.AddressLine2).IsRequired();
            builder.Property(e => e.City).IsRequired();
            builder.Property(e => e.CountaryId).IsRequired();
            builder.Property(e => e.PostalCode).IsRequired();
            builder.Property(e => e.Region).IsRequired();
            builder.Property(e => e.StreetNumber).IsRequired();
            builder.Property(e => e.UnitNumber).IsRequired();
        }
    }
}
