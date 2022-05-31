using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndAnySellDataAccess.Migrations
{
    public partial class addpriceBalansProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "BalanceProducts",
                newName: "BalanceCount");

            migrationBuilder.AddColumn<decimal>(
                name: "ComingPrice",
                table: "BalanceProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComingPrice",
                table: "BalanceProducts");

            migrationBuilder.RenameColumn(
                name: "BalanceCount",
                table: "BalanceProducts",
                newName: "Balance");
        }
    }
}
