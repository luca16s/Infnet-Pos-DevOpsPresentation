using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeadFishStudio.Product.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "deadfish");

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                schema: "deadfish",
                columns: table => new
                {
                    PDCT_SQ_PRODUCT = table.Column<Guid>(nullable: false),
                    PDCT_NAME = table.Column<string>(nullable: false),
                    PDCT_QUANTITY = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => x.PDCT_SQ_PRODUCT);
                });

            migrationBuilder.CreateTable(
                name: "PRICE",
                schema: "deadfish",
                columns: table => new
                {
                    PRCE_IN_ACTIVE = table.Column<bool>(nullable: false),
                    PRCE_CURRENCY = table.Column<string>(nullable: false),
                    PRCE_AMOUNT = table.Column<decimal>(nullable: false),
                    PRCE_DT_CREATED = table.Column<DateTime>(nullable: false),
                    PDCT_SQ_PRODUCT = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRICE", x => x.PRCE_IN_ACTIVE);
                    table.ForeignKey(
                        name: "FK_PRICE_PRODUCT_PDCT_SQ_PRODUCT",
                        column: x => x.PDCT_SQ_PRODUCT,
                        principalSchema: "deadfish",
                        principalTable: "PRODUCT",
                        principalColumn: "PDCT_SQ_PRODUCT",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRICE_PDCT_SQ_PRODUCT",
                schema: "deadfish",
                table: "PRICE",
                column: "PDCT_SQ_PRODUCT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRICE",
                schema: "deadfish");

            migrationBuilder.DropTable(
                name: "PRODUCT",
                schema: "deadfish");
        }
    }
}
