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
                    PDCT_NAME = table.Column<string>(nullable: true),
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
                    PDCT_SQ_PRODUCT = table.Column<Guid>(nullable: false),
                    PRCE_CURRENCY = table.Column<string>(nullable: true),
                    PRCE_AMOUNT = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRICE", x => x.PDCT_SQ_PRODUCT);
                    table.ForeignKey(
                        name: "FK_PRICE_PRODUCT_PDCT_SQ_PRODUCT",
                        column: x => x.PDCT_SQ_PRODUCT,
                        principalSchema: "deadfish",
                        principalTable: "PRODUCT",
                        principalColumn: "PDCT_SQ_PRODUCT",
                        onDelete: ReferentialAction.Cascade);
                });
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
