using Microsoft.EntityFrameworkCore.Migrations;

namespace DeadFishStudio.MarketList.Infrastructure.Data.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ITEMS",
                schema: "deadfish",
                table: "ITEMS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ITEMS",
                schema: "deadfish",
                table: "ITEMS",
                column: "MKLT_SQ_PRODUCT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ITEMS",
                schema: "deadfish",
                table: "ITEMS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ITEMS",
                schema: "deadfish",
                table: "ITEMS",
                column: "MKLT_SQ_MARKET_LIST");
        }
    }
}
