using Microsoft.EntityFrameworkCore.Migrations;

namespace BabzRent.Migrations
{
    public partial class UpdateToBills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Bills",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Usage",
                table: "Bills",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "UsageIncluded",
                table: "Bills",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Usage",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "UsageIncluded",
                table: "Bills");
        }
    }
}
