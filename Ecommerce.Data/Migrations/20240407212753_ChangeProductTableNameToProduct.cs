using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductTableNameToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItem_ProductTable_ProductId",
                table: "ProductItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTable_Category_CategoryId",
                table: "ProductTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTable",
                table: "ProductTable");

            migrationBuilder.RenameTable(
                name: "ProductTable",
                newName: "Product");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTable_Id",
                table: "Product",
                newName: "IX_Product_Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTable_CategoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItem_Product_ProductId",
                table: "ProductItem",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductItem_Product_ProductId",
                table: "ProductItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "ProductTable");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Id",
                table: "ProductTable",
                newName: "IX_ProductTable_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "ProductTable",
                newName: "IX_ProductTable_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTable",
                table: "ProductTable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItem_ProductTable_ProductId",
                table: "ProductItem",
                column: "ProductId",
                principalTable: "ProductTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTable_Category_CategoryId",
                table: "ProductTable",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
