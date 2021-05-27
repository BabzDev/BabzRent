using Microsoft.EntityFrameworkCore.Migrations;

namespace BabzRent.Migrations
{
    public partial class BillTypeUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnitType",
                table: "BillTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitTypePL",
                table: "BillTypes",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "UnitCost",
                table: "Bills",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitType",
                table: "Bills",
                nullable: true);

            migrationBuilder.Sql("UPDATE BillTypes SET UnitType='Month', UnitTypePL='Miesiąc' WHERE Id=1;");
            migrationBuilder.Sql("UPDATE BillTypes SET UnitType='Month', UnitTypePL='Miesiąc' WHERE Id=2;");
            migrationBuilder.Sql("UPDATE BillTypes SET UnitType='KWh', UnitTypePL='KWh' WHERE Id=3;");
            migrationBuilder.Sql("UPDATE BillTypes SET UnitType='m3', UnitTypePL='m3' WHERE Id=4;");
            migrationBuilder.Sql("UPDATE BillTypes SET UnitType='m3', UnitTypePL='m3' WHERE Id=5;");
            migrationBuilder.Sql("UPDATE BillTypes SET UnitType='m3', UnitTypePL='m3' WHERE Id=6;");
            migrationBuilder.Sql("UPDATE BillTypes SET UnitType='Month', UnitTypePL='Miesiąc' WHERE Id=7;");
            migrationBuilder.Sql("UPDATE BillTypes SET UnitType='Month', UnitTypePL='Miesiąc' WHERE Id=8;");
            migrationBuilder.Sql("UPDATE BillTypes SET UnitType='Month', UnitTypePL='Miesiąc' WHERE Id=9;");
            migrationBuilder.Sql("UPDATE BillTypes SET UnitType='Month', UnitTypePL='Miesiąc' WHERE Id=10;");
            migrationBuilder.Sql("UPDATE BillTypes SET UnitType='Month', UnitTypePL='Miesiąc' WHERE Id=11;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "UnitTypePL",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "UnitCost",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "Bills");
        }
    }
}
