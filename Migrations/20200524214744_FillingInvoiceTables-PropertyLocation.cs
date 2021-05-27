using Microsoft.EntityFrameworkCore.Migrations;

namespace BabzRent.Migrations
{
    public partial class FillingInvoiceTablesPropertyLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO InvoiceStatus (Id, Description, DescriptionPL) VALUES 
(0, 'Not yet Invoiced', 'Jescze nie pobrany'),
(1, 'Invoiced', 'Dodany do Faktury'),
(2, 'Bill Update - Adjustment Not Yet Invoiced', 'Poprawiony - Jescze nie pobrany'),
(3, 'Bill Update - Adjustment Invoiced', 'Poprawiony - Pobrany'),
(4, 'Bill Update - No Adjustment', 'Poprawiony - Bez zmian'),
(5, 'Written Off Not Paid', 'Koszty - Nie zaplacony'),
(6, 'Written Off - Maintenance Cost', 'Koszty - Utrzymanie po za wynajmem'),
(11, 'System - Initial Meter Creation', 'System - Kreacja Licznika');
");

            migrationBuilder.Sql(@"
INSERT INTO PropertyLocations (Id, ShortCode, BuildingName, Street, AddressLine2, City, PostCode, County, Country) VALUES (1, N'GN8', N'8', N'Gnieznienska', NULL, N'Lodz', NULL, NULL, N'Poland');
INSERT INTO PropertyLocations (Id, ShortCode, BuildingName, Street, AddressLine2, City, PostCode, County, Country) VALUES (2, N'PIAS9', N'9', N'Piaseczna', NULL, N'Lodz', NULL, NULL, N'Poland');
INSERT INTO PropertyLocations (Id, ShortCode, BuildingName, Street, AddressLine2, City, PostCode, County, Country) VALUES (3, N'HAR.76a', N'76a', N'Harlington Road', NULL, N'Feltham', NULL, NULL, N'UK');
			");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
