﻿// <auto-generated />
using System;
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240408151653_ChangeProductItemPriceInputToFloat")]
    partial class ChangeProductItemPriceInputToFloat
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Product Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Product Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.ProductCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Category Name");

                    b.Property<string>("ParentCategoryId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Parent Category Id");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.ProductImages", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.ProductItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Price")
                        .HasColumnType("real")
                        .HasColumnName("Product Item Price");

                    b.Property<string>("ProducItemImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Produc Item Image Url");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int")
                        .HasColumnName("Quantity In Stock");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Stock keeping unit (SKU)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductItem");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.ProductVariation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VariationOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductItemId");

                    b.HasIndex("VariationOptionId");

                    b.ToTable("ProductVariation");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.Variation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Variation Title");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Variation");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.VariationOptions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Variation Name");

                    b.Property<Guid>("VariationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("VariationId");

                    b.ToTable("VariationOptions");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.Product", b =>
                {
                    b.HasOne("Ecommerce.Data.Models.Entities.ProductCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.ProductImages", b =>
                {
                    b.HasOne("Ecommerce.Data.Models.Entities.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.ProductItem", b =>
                {
                    b.HasOne("Ecommerce.Data.Models.Entities.Product", "Product")
                        .WithMany("ProductItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.ProductVariation", b =>
                {
                    b.HasOne("Ecommerce.Data.Models.Entities.ProductItem", "ProductItem")
                        .WithMany("ProductVariation2")
                        .HasForeignKey("ProductItemId");

                    b.HasOne("Ecommerce.Data.Models.Entities.VariationOptions", "VariationOption")
                        .WithMany("ProductVariation1")
                        .HasForeignKey("VariationOptionId");

                    b.Navigation("ProductItem");

                    b.Navigation("VariationOption");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.Variation", b =>
                {
                    b.HasOne("Ecommerce.Data.Models.Entities.ProductCategory", "Category")
                        .WithMany("Variations")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.VariationOptions", b =>
                {
                    b.HasOne("Ecommerce.Data.Models.Entities.Variation", "Variation")
                        .WithMany("VariationOptions")
                        .HasForeignKey("VariationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Variation");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.Product", b =>
                {
                    b.Navigation("ProductImages");

                    b.Navigation("ProductItems");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.ProductCategory", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Variations");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.ProductItem", b =>
                {
                    b.Navigation("ProductVariation2");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.Variation", b =>
                {
                    b.Navigation("VariationOptions");
                });

            modelBuilder.Entity("Ecommerce.Data.Models.Entities.VariationOptions", b =>
                {
                    b.Navigation("ProductVariation1");
                });
#pragma warning restore 612, 618
        }
    }
}
