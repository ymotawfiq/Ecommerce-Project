using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAddressesToDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_Address_AddressId",
                table: "UserAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_AspNetUsers_UserId1",
                table: "UserAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddress",
                table: "UserAddress");

            migrationBuilder.DropIndex(
                name: "IX_UserAddress_UserId1",
                table: "UserAddress");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0702109b-77cd-49d9-aaab-32914171968c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be126021-edfc-4303-8e80-a360f6f69a2b");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserAddress");

            migrationBuilder.RenameTable(
                name: "UserAddress",
                newName: "UserAddresses");

            migrationBuilder.RenameColumn(
                name: "IsDefault",
                table: "UserAddresses",
                newName: "Is Address Default");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddress_AddressId",
                table: "UserAddresses",
                newName: "IX_UserAddresses_AddressId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserAddresses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37fcdfab-c386-4a34-84ae-e512a5887277", "1", "Admin", "Admin" },
                    { "7630ea8b-f7e9-4938-8c49-6093855f86f0", "2", "User", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Address_AddressId",
                table: "UserAddresses",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_AspNetUsers_UserId",
                table: "UserAddresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Address_AddressId",
                table: "UserAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_AspNetUsers_UserId",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37fcdfab-c386-4a34-84ae-e512a5887277");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7630ea8b-f7e9-4938-8c49-6093855f86f0");

            migrationBuilder.RenameTable(
                name: "UserAddresses",
                newName: "UserAddress");

            migrationBuilder.RenameColumn(
                name: "Is Address Default",
                table: "UserAddress",
                newName: "IsDefault");

            migrationBuilder.RenameIndex(
                name: "IX_UserAddresses_AddressId",
                table: "UserAddress",
                newName: "IX_UserAddress_AddressId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserAddress",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserAddress",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddress",
                table: "UserAddress",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0702109b-77cd-49d9-aaab-32914171968c", "1", "Admin", "Admin" },
                    { "be126021-edfc-4303-8e80-a360f6f69a2b", "2", "User", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_UserId1",
                table: "UserAddress",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_Address_AddressId",
                table: "UserAddress",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_AspNetUsers_UserId1",
                table: "UserAddress",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
