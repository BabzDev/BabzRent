using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Billing;
using BabzRent.Data;
using BabzRent.Models.Expenses;
using Microsoft.EntityFrameworkCore;

namespace BabzRent.Models.Reports
{

    public class KPIR
    {
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double? Income { get; set; }
        public double? Expense { get; set; }


        public static IEnumerable<KPIR> ExpenseAndInvoicesToKPIR(IEnumerable<Invoice> invoicesInclTenancyAndTenant, IEnumerable<Expense> expensesInclExpenseLocationAndCompany)
        {
            var invoices = invoicesInclTenancyAndTenant.Select(i =>
                new KPIR()
                {
                    Date = i.DateIssued.GetValueOrDefault(),
                    Reference = i.Reference,
                    Name = i.Tenancy.Tenant.FirstName + " " + i.Tenancy.Tenant.LastName,
                    Description = "Przychod",
                    Income = i.Amount
                }
            ).ToList();

            var expense = expensesInclExpenseLocationAndCompany.Select(e =>
                new KPIR() 
                { 
                    Date = e.ExpenseDate,
                    Reference = e.Reference,
                    Name = e.Company.Name,
                    Address = e.ExpenseLocation.Address1,
                    Description  = e.Description,
                    Expense = e.Amount
                }
            ).ToList();

            var KPIR = invoices.Concat(expense).OrderBy(i => i.Date);
            return KPIR;
        }
    }
}