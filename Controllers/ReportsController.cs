using BabzRent.Models;
using BabzRent.Models.Billing;
using BabzRent.Models.Reports;
using BabzRent.ViewModels.ReportsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabzRent.Data;
using BabzRent.Models.Tenancies;
using BabzRent.Models.Expenses;
using Microsoft.CodeAnalysis;

namespace BabzRent.Controllers
{
    public class ReportsController : Controller
    {
        public ApplicationDbContext _context { get; set; }

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reports
        public ActionResult Index()
        {
            return View(); 
        }

        public ActionResult KPIRYearForm()
        {
            List<int> dates = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                dates.Add(DateTime.Now.Year - i);
            };

            var viewModel = new KPIRYearFormViewModel
            {
                Locations = _context.PropertyLocations.ToList(),
                Dates = dates
            };

            return View(viewModel);
        }

        public ActionResult KPIRYearlyView(int locationId, int? financialYear)
        {
            IEnumerable<Invoice> invoices;
            IEnumerable<Expense> expenses;
            var invoice = new Invoice(_context);
            var expense = new Expense(_context);

            if (financialYear != null)
            {
                invoices = invoice.GetAllInvoicesAtLocationForTaxYear(locationId, (int)financialYear);
                expenses = expense.GetAllExpensesAtLocationForTaxYear(locationId, (int)financialYear);
            }
            else
            {
                invoices = invoice.GetAllInvoicesAtLocation(locationId);
                expenses = expense.GetAllExpensesAtLocation(locationId);
            }

            var viewModel = new KPIRViewModel()
            {
                KPIRs = KPIR.ExpenseAndInvoicesToKPIR(invoices, expenses),
                Tenancies = TenancyNames.Get(_context.Tenancies.Include(m => m.Property).ToList()),
            };
            ViewBag.ReportingYear = financialYear;
            ViewBag.Location = _context.PropertyLocations.Single(l => l.Id == locationId).ShortCode;
            return View(viewModel);
        }
    }
}