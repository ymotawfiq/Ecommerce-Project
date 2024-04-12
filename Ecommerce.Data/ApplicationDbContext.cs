

using Ecommerce.Data.EntityConfigurations;
using Ecommerce.Data.Models.Entities;
using Ecommerce.Data.Models.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<SiteUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ApplyEntitiesConfigurations(modelBuilder);
            SeedRoles(modelBuilder);
        }

        private void ApplyEntitiesConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration())
                .ApplyConfiguration(new ProductConfiguration())
                .ApplyConfiguration(new ProductItemConfiguration())
                .ApplyConfiguration(new ProductVariationConfiguration())
                .ApplyConfiguration(new VariationConfigurations())
                .ApplyConfiguration(new VariationOptionsConfiguration())
                .ApplyConfiguration(new ProductImagesConfigurations())
                .ApplyConfiguration(new PromotionConfiguration())
                .ApplyConfiguration(new PromotionCategoryConfiguration())
                .ApplyConfiguration(new CountaryConfiguration())
                .ApplyConfiguration(new AddressConfiguration())
                .ApplyConfiguration(new SiteUserConfiguration())
                .ApplyConfiguration(new UserAddressConnfiguration());
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { ConcurrencyStamp="1", Name = "Admin", NormalizedName = "Admin"},
                new IdentityRole() { ConcurrencyStamp="2", Name = "User", NormalizedName="User"}
                );
        }

        public DbSet<ProductCategory> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductItem> ProductItem { get; set; }
        public DbSet<ProductVariation> ProductVariation { get; set; }
        public DbSet<Variation> Variation { get; set; }
        public DbSet<VariationOptions> VariationOptions { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<PromotionCategory> PromotionCategory { get; set; }
        public DbSet<Countary> Countary { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
    }
}
