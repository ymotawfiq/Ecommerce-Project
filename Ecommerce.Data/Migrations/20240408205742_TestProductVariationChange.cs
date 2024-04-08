using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class TestProductVariationChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariation_ProductItem_ProductItemId",
                table: "ProductVariation");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariation_ProductItem_ProductItemId",
                table: "ProductVariation",
                column: "ProductItemId",
                principalTable: "ProductItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariation_ProductItem_ProductItemId",
                table: "ProductVariation");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariation_ProductItem_ProductItemId",
                table: "ProductVariation",
                column: "ProductItemId",
                principalTable: "ProductItem",
                principalColumn: "Id");
        }
    }
}
