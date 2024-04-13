using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPaymentMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8de37dbc-3332-4f5a-a984-633a69734922");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de6f4587-b02d-4f54-adeb-ad054d554a71");

            migrationBuilder.CreateTable(
                name: "UserPaymentMethod",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(name: "Account Number", type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(name: "Expiration Date", type: "datetime2", nullable: false),
                    IsRequiredPaymentMethod = table.Column<bool>(name: "Is Required Payment Method", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPaymentMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPaymentMethod_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPaymentMethod_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "69af6664-065a-49f7-aa7b-f62ddaa35357", "1", "Admin", "Admin" },
                    { "d99628f4-37f2-4afe-bd5c-c79fca9aedf7", "2", "User", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPaymentMethod_PaymentTypeId",
                table: "UserPaymentMethod",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPaymentMethod_UserId",
                table: "UserPaymentMethod",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPaymentMethod");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69af6664-065a-49f7-aa7b-f62ddaa35357");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d99628f4-37f2-4afe-bd5c-c79fca9aedf7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8de37dbc-3332-4f5a-a984-633a69734922", "1", "Admin", "Admin" },
                    { "de6f4587-b02d-4f54-adeb-ad054d554a71", "2", "User", "User" }
                });
        }
    }
}
