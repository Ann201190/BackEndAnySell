using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndAnySellDataAccess.Migrations
{
    public partial class addemployeeidProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Providers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Providers_EmployeeId",
                table: "Providers",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_Employees_EmployeeId",
                table: "Providers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_Employees_EmployeeId",
                table: "Providers");

            migrationBuilder.DropIndex(
                name: "IX_Providers_EmployeeId",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Providers");
        }
    }
}
