

using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class UserAddressConnfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.User).WithMany(e => e.UserAddresses).HasForeignKey(e => e.UserId);
            builder.HasOne(e => e.Address).WithMany(e => e.UserAddresses).HasForeignKey(e => e.AddressId)
                .IsRequired(false);
            builder.Property(e => e.IsDefault).IsRequired().HasColumnName("Is Address Default");
        }
    }
}
