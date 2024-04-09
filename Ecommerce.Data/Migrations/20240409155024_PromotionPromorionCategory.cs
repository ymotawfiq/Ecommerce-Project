using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class PromotionPromotionCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionName = table.Column<string>(name: "Promotion Name", type: "nvarchar(max)", nullable: false),
                    PromotionDescription = table.Column<string>(name: "Promotion Description", type: "nvarchar(max)", nullable: false),
                    DiscountRate = table.Column<float>(type: "real", nullable: false),
                    PromotionStartingDate = table.Column<DateTime>(name: "Promotion Starting Date", type: "datetime2", nullable: false),
                    PromotionEndingDate = table.Column<DateTime>(name: "Promotion Ending Date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromotionCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PromotionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PromotionCategory_Promotion_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCategory_CategoryId",
                table: "PromotionCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCategory_PromotionId",
                table: "PromotionCategory",
                column: "PromotionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromotionCategory");

            migrationBuilder.DropTable(
                name: "Promotion");
        }
    }
}
