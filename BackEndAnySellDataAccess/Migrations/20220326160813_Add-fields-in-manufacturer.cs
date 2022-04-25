using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppartmentAppDataAccess.Migrations
{
    public partial class Addfieldsinmanufacturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Manufacturers");

            migrationBuilder.AddColumn<Guid>(
                name: "AdressId",
                table: "Manufacturers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Manufacturers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Adress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_AdressId",
                table: "Manufacturers",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_OwnerId",
                table: "Manufacturers",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacturers_Adress_AdressId",
                table: "Manufacturers",
                column: "AdressId",
                principalTable: "Adress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacturers_Owners_OwnerId",
                table: "Manufacturers",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manufacturers_Adress_AdressId",
                table: "Manufacturers");

            migrationBuilder.DropForeignKey(
                name: "FK_Manufacturers_Owners_OwnerId",
                table: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Adress");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_AdressId",
                table: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_OwnerId",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "AdressId",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Manufacturers");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Manufacturers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
