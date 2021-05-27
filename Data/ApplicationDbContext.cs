using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BabzRent.Models.Maintenance;
using BabzRent.Models.Tenancies;
using BabzRent.Models.Billing;
using BabzRent.Models.Tenants;
using BabzRent.Models.Expenses;
using BabzRent.Models.Properties;
using BabzRent.Models;

namespace BabzRent.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyLocation> PropertyLocations { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Tenancy> Tenancies { get; set; }
        public DbSet<Meter> Meters { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<BillType> BillTypes { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<InvoiceStatus> InvoiceStatus { get; set; }
        public DbSet<UtilityCosts> UtilityCosts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCompany> ExpenseCompanies { get; set; }
        public DbSet<ExpenseCompanyLocation> ExpenseCompanyLocations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
