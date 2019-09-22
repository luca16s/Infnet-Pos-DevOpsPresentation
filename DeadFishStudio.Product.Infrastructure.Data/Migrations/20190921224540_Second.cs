using Microsoft.EntityFrameworkCore.Migrations;

namespace DeadFishStudio.Product.Infrastructure.Data.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PRICE",
                schema: "deadfish",
                table: "PRICE");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRICE",
                schema: "deadfish",
                table: "PRICE",
                columns: new[] { "PRCE_IN_ACTIVE", "PRCE_DT_CREATED" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PRICE",
                schema: "deadfish",
                table: "PRICE");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PRICE",
                schema: "deadfish",
                table: "PRICE",
                column: "PRCE_IN_ACTIVE");
        }
    }
}
