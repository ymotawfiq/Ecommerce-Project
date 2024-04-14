using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddShopOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfc194f3-9fa9-4645-a085-d4541c572b4d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e18b5207-ab8f-4ea6-bfcb-d4483fcdaaca");

            migrationBuilder.CreateTable(
                name: "ShopOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(name: "Order Date", type: "datetime2", nullable: false),
                    TotalOrderPrice = table.Column<decimal>(name: "Total Order Price", type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopOrder_Address_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopOrder_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopOrder_OrderStatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopOrder_ShippingMethod_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "ShippingMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopOrder_UserPaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "UserPaymentMethod",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6e5f4e53-1c5d-4884-9375-d26844867cad", "2", "User", "User" },
                    { "86a21b35-8c66-4113-beeb-2ef5733a123e", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopOrder_OrderStatusId",
                table: "ShopOrder",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopOrder_PaymentMethodId",
                table: "ShopOrder",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopOrder_ShippingAddressId",
                table: "ShopOrder",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopOrder_ShippingMethodId",
                table: "ShopOrder",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopOrder_UserId",
                table: "ShopOrder",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopOrder");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e5f4e53-1c5d-4884-9375-d26844867cad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86a21b35-8c66-4113-beeb-2ef5733a123e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "dfc194f3-9fa9-4645-a085-d4541c572b4d", "1", "Admin", "Admin" },
                    { "e18b5207-ab8f-4ea6-bfcb-d4483fcdaaca", "2", "User", "User" }
                });
        }
    }
}
