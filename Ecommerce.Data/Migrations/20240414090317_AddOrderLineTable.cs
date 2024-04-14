using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderLineTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e5f4e53-1c5d-4884-9375-d26844867cad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86a21b35-8c66-4113-beeb-2ef5733a123e");

            migrationBuilder.CreateTable(
                name: "OrderLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Orderquantity = table.Column<int>(name: "Order quantity", type: "int", nullable: false),
                    OrderPrice = table.Column<decimal>(name: "Order Price", type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLine_ProductItem_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderLine_ShopOrder_ShopOrderId",
                        column: x => x.ShopOrderId,
                        principalTable: "ShopOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28a36663-369b-4312-b61e-9ec13891fe16", "2", "User", "User" },
                    { "30deb934-da8e-46cc-b211-b796ef0270df", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_ProductItemId",
                table: "OrderLine",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLine_ShopOrderId",
                table: "OrderLine",
                column: "ShopOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLine");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28a36663-369b-4312-b61e-9ec13891fe16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30deb934-da8e-46cc-b211-b796ef0270df");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6e5f4e53-1c5d-4884-9375-d26844867cad", "2", "User", "User" },
                    { "86a21b35-8c66-4113-beeb-2ef5733a123e", "1", "Admin", "Admin" }
                });
        }
    }
}
