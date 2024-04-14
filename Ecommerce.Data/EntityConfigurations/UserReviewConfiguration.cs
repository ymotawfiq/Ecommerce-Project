

using Ecommerce.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Data.EntityConfigurations
{
    public class UserReviewConfiguration : IEntityTypeConfiguration<UserReview>
    {
        public void Configure(EntityTypeBuilder<UserReview> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.OrderLine).WithMany(e => e.UserReview)
                .HasForeignKey(e => e.OrderId).IsRequired(false);
            builder.HasOne(e => e.User).WithMany(e => e.UserReview).HasForeignKey(e => e.UserId);
            builder.Property(e => e.Rate).IsRequired().HasColumnName("User Rate");
            builder.Property(e => e.Comment).IsRequired().HasColumnName("User Comment");
        }
    }
}
