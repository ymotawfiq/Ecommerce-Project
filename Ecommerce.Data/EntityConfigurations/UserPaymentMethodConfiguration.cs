

using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class UserPaymentMethodConfiguration : IEntityTypeConfiguration<UserPaymentMethod>
    {
        public void Configure(EntityTypeBuilder<UserPaymentMethod> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.PaymentType).WithMany(e => e.UserPaymentMethods)
                .HasForeignKey(e => e.PaymentTypeId);
            builder.HasOne(e => e.User).WithMany(e => e.UserPaymentMethods).HasForeignKey(e => e.UserId);
            builder.Property(e => e.AccountNumber).IsRequired().HasColumnName("Account Number");
            builder.Property(e => e.ExpiryDate).IsRequired().HasColumnName("Expiration Date");
            builder.Property(e => e.IsDefault).IsRequired().HasColumnName("Is Required Payment Method");
            builder.Property(e => e.Provider).IsRequired();
            
        }
    }
}
