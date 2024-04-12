using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeRegisterInputDoesnotRequirePhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af89cbe2-6944-46cd-b952-fd9cb41282e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f60c24e7-a700-4b05-95b3-d1db563710c5");

            migrationBuilder.RenameColumn(
                name: "Phone Number",
                table: "AspNetUsers",
                newName: "PhoneNumber");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "61a0a9f0-12e3-4818-907b-7245b4171e73", "1", "Admin", "Admin" },
                    { "ce56d841-20ce-40c1-b409-fd6a68d5f9b1", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61a0a9f0-12e3-4818-907b-7245b4171e73");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce56d841-20ce-40c1-b409-fd6a68d5f9b1");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "AspNetUsers",
                newName: "Phone Number");

            migrationBuilder.AlterColumn<string>(
                name: "Phone Number",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "af89cbe2-6944-46cd-b952-fd9cb41282e6", "1", "Admin", "Admin" },
                    { "f60c24e7-a700-4b05-95b3-d1db563710c5", "2", "User", "User" }
                });
        }
    }
}
