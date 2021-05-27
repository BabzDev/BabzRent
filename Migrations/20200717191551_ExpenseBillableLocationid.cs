using Microsoft.EntityFrameworkCore.Migrations;

namespace BabzRent.Migrations
{
    public partial class ExpenseBillableLocationid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseCompanyLocations_LocationId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_LocationId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "BillableLocationId",
                table: "Expenses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillablePropertyId",
                table: "Expenses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExpenseLocationId",
                table: "Expenses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_BillableLocationId",
                table: "Expenses",
                column: "BillableLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_BillablePropertyId",
                table: "Expenses",
                column: "BillablePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseLocationId",
                table: "Expenses",
                column: "ExpenseLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_PropertyLocations_BillableLocationId",
                table: "Expenses",
                column: "BillableLocationId",
                principalTable: "PropertyLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Properties_BillablePropertyId",
                table: "Expenses",
                column: "BillablePropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseCompanyLocations_ExpenseLocationId",
                table: "Expenses",
                column: "ExpenseLocationId",
                principalTable: "ExpenseCompanyLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_PropertyLocations_BillableLocationId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Properties_BillablePropertyId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseCompanyLocations_ExpenseLocationId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_BillableLocationId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_BillablePropertyId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_ExpenseLocationId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "BillableLocationId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "BillablePropertyId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "ExpenseLocationId",
                table: "Expenses");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Expenses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_LocationId",
                table: "Expenses",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseCompanyLocations_LocationId",
                table: "Expenses",
                column: "LocationId",
                principalTable: "ExpenseCompanyLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
