using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mango.WEB.Migrations
{
    public partial class UpdatedStockTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserUID",
                table: "Stocks",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserUID",
                table: "Stocks");
        }
    }
}
