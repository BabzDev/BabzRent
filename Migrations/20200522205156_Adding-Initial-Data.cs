using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Data.Sql;
using System;
using BabzRent.Data;


namespace BabzRent.Migrations
{
    public partial class AddingInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO PropertyTypes (Id, Name, Bedrooms, Bathrooms) VALUES (0, 'Studio', 1, 1)");
            migrationBuilder.Sql("INSERT INTO PropertyTypes (Id, Name, Bedrooms, Bathrooms) VALUES (1, '1 Bed Flat', 1, 1)");
            migrationBuilder.Sql("INSERT INTO PropertyTypes (Id, Name, Bedrooms, Bathrooms) VALUES (2, '2 Bed Flat', 2, 1)");
            migrationBuilder.Sql("INSERT INTO PropertyTypes (Id, Name, Bedrooms, Bathrooms) VALUES (3, '3 Bed Flat', 3, 1)");

            migrationBuilder.Sql("INSERT INTO BillTypes (Id, Name, NamePL) VALUES (1, 'Rent', 'Wynajem')");
            migrationBuilder.Sql("INSERT INTO BillTypes (Id, Name, NamePL) VALUES (2, 'Service Charge', 'Czynsz')");
            migrationBuilder.Sql("INSERT INTO BillTypes (Id, Name, NamePL) VALUES (3, 'Electric (Metered)', 'Prad (Licznik)')");
            migrationBuilder.Sql("INSERT INTO BillTypes (Id, Name, NamePL) VALUES (4, 'Gas (Meterd)', 'Gaz (Licznik)')");
            migrationBuilder.Sql("INSERT INTO BillTypes (Id, Name, NamePL) VALUES (5, 'Water (Metered)', 'Woda (Licznik)')");
            migrationBuilder.Sql("INSERT INTO BillTypes (Id, Name, NamePL) VALUES (6, 'Hot Water (Metered)', 'Ciepla Woda (Licznik)')");
            migrationBuilder.Sql("INSERT INTO BillTypes (Id, Name, NamePL) VALUES (7, 'Rubbish Collection', 'Smieci')");
            migrationBuilder.Sql("INSERT INTO BillTypes (Id, Name, NamePL) VALUES (8, 'Electric', 'Prad')");
            migrationBuilder.Sql("INSERT INTO BillTypes (Id, Name, NamePL) VALUES (9, 'Gas', 'Gaz')");
            migrationBuilder.Sql("INSERT INTO BillTypes (Id, Name, NamePL) VALUES (10, 'Water', 'Woda')");
            migrationBuilder.Sql("INSERT INTO BillTypes (Id, Name, NamePL) VALUES (11, 'Hot Water', 'Ciepla Woda')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
