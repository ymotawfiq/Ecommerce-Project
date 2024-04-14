using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserReviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28a36663-369b-4312-b61e-9ec13891fe16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30deb934-da8e-46cc-b211-b796ef0270df");

            migrationBuilder.CreateTable(
                name: "UserReview",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserRate = table.Column<int>(name: "User Rate", type: "int", nullable: false),
                    UserComment = table.Column<string>(name: "User Comment", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReview_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserReview_OrderLine_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderLine",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28f9f787-704a-4a74-9ab1-39329b0298b5", "1", "Admin", "Admin" },
                    { "9fb00e11-1494-4dd8-815c-d6174f350c58", "2", "User", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserReview_OrderId",
                table: "UserReview",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReview_UserId",
                table: "UserReview",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserReview");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28f9f787-704a-4a74-9ab1-39329b0298b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fb00e11-1494-4dd8-815c-d6174f350c58");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28a36663-369b-4312-b61e-9ec13891fe16", "2", "User", "User" },
                    { "30deb934-da8e-46cc-b211-b796ef0270df", "1", "Admin", "Admin" }
                });
        }
    }
}
