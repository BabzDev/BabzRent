using Microsoft.EntityFrameworkCore.Migrations;

namespace BabzRent.Migrations
{
    public partial class KPIRUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Invoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Invoices");
        }
    }
}
