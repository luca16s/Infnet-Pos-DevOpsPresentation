using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeadFishStudio.MarketList.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "deadfish");

            migrationBuilder.CreateTable(
                name: "ITEMS",
                schema: "deadfish",
                columns: table => new
                {
                    MKLT_SQ_MARKET_LIST = table.Column<Guid>(nullable: false),
                    MKLT_SQ_PRODUCT = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITEMS", x => x.MKLT_SQ_MARKET_LIST);
                });

            migrationBuilder.CreateTable(
                name: "MARKETLIST",
                schema: "deadfish",
                columns: table => new
                {
                    MKLT_SQ_MARKET_LIST = table.Column<Guid>(nullable: false),
                    MKLT_NM_MARKET_LIST = table.Column<string>(nullable: false),
                    MKLT_DT_MARKET_LIST = table.Column<DateTime>(nullable: false),
                    DataDeModificacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MARKETLIST", x => x.MKLT_SQ_MARKET_LIST);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITEMS",
                schema: "deadfish");

            migrationBuilder.DropTable(
                name: "MARKETLIST",
                schema: "deadfish");
        }
    }
}
