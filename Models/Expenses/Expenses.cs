using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BabzRent.Data;
using BabzRent.Models.Properties;
using BabzRent.ViewModels.ExpensesViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BabzRent.Models.Expenses
{
    public class Expense
    {
        public int Id { get; set; }
        public ExpenseCompany Company { get; set; }
        public int CompanyId { get; set; }
        public double Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public DateTime? DateSettled { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public ExpenseCompanyLocation ExpenseLocation { get; set; }
        public int ExpenseLocationId { get; set; }
        public PropertyLocation BillableLocation { get; set; }
        public int? BillableLocationId { get; set; }
        public Property BillableProperty { get; set; }
        public int? BillablePropertyId { get; set; }

        [NotMapped]
        private ApplicationDbContext _context {get; set;}

        public Expense()
        {

        }

        public Expense(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }


        public IEnumerable<Expense> GetAllExpensesAtLocationForTaxYear(int locationId, int taxYear)
        {
            var startTaxYearDate = new DateTime(taxYear, 1, 1);
            var endTaxYearDate = new DateTime(taxYear, 12, 31);
            var expenses = _context.Expenses
                .Where(i => i.ExpenseDate >= startTaxYearDate & i.ExpenseDate <= endTaxYearDate & i.BillableLocationId==locationId & i.ExpenseDate != null)
                .Include(i => i.ExpenseLocation)
                .Include(i => i.Company)
                .ToList();
            return expenses;
        }

        public IEnumerable<Expense> GetAllExpensesAtLocation(int locationId)
        {
            var expenseList = _context.Expenses.Where(i => i.BillableLocationId == locationId)
                .Include(i => i.ExpenseLocation)
                .Include(i => i.Company)
                .ToList();
            return expenseList;
        }

        public void SaveExpense(Expense expense)
        {
            if (expense.Id == 0)
            {
                _context.Expenses.Add(expense);
                _context.SaveChanges();
            }
            else
            {
                var expenseInDb = _context.Expenses.Single(e => e.Id == expense.Id);
                expenseInDb.Amount = expense.Amount;
                expenseInDb.BillableLocationId = expense.BillableLocationId;
                expenseInDb.BillablePropertyId = expense.BillablePropertyId;
                expenseInDb.CompanyId = expense.CompanyId;
                expenseInDb.DateSettled = expense.DateSettled;
                expenseInDb.Description = expense.Description;
                expenseInDb.ExpenseDate = expense.ExpenseDate;
                expenseInDb.ExpenseLocationId = expense.ExpenseLocationId;
                expenseInDb.Reference = expense.Reference;
                _context.SaveChanges();
            }
        }
    }

    public class ExpenseCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VATReference { get; set; }

        [NotMapped]
        private ApplicationDbContext _context { get; set; }

        public ExpenseCompany()
        {

        }

        public ExpenseCompany(ApplicationDbContext context)
        {
            _context = context;

        }


        public void SaveCompany(CompanyFormViewModel viewModel)
        {
            if (viewModel.Company.Id == 0)
            {
                _context.ExpenseCompanies.Add(viewModel.Company);
                _context.SaveChanges();
                viewModel.CompanyLocation.CompanyId = viewModel.Company.Id;
                _context.ExpenseCompanyLocations.Add(viewModel.CompanyLocation);
                _context.SaveChanges();
            }
            else
            {
                var companyInDB = _context.ExpenseCompanies.Single(c => c.Id == viewModel.Company.Id);
                companyInDB.Name = viewModel.Company.Name;
                companyInDB.VATReference = viewModel.Company.VATReference;
                _context.SaveChanges();
            }
        }
    }

    public class ExpenseCompanyLocation
    {
        public int Id { get; set; }
        public ExpenseCompany Company { get; set; }
        public int CompanyId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        [NotMapped]
        private ApplicationDbContext _context { get; set; }

        public ExpenseCompanyLocation()
        {
        }

        public ExpenseCompanyLocation(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public static IEnumerable<SelectListItem> GetLocationsSelectList()
        { //Used to initially populate the Views - as View will require a Select List (even if empty)
            List<SelectListItem> locations = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };

            return locations;
        }

        public IEnumerable<SelectListItem> GetLocationsSelectList(int companyId)
        {
            if (companyId != 0)
            {
                IEnumerable<SelectListItem> locations = _context.ExpenseCompanyLocations.AsNoTracking()
                    .OrderBy(l => l.Address1)
                    .Where(l => l.CompanyId == companyId)
                    .Select(n =>
                    new SelectListItem
                    {
                        Value = n.Id.ToString(),
                        Text = n.Address1
                    })
                    .ToList();
                return locations;
            };
            return null;
        }

    }
}