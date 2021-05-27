using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabzRent.Data;
using BabzRent.Models.Expenses;
using BabzRent.ViewModels.ExpensesViewModels;
using Org.BouncyCastle.Asn1.Cms.Ecc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using BabzRent.Models.Properties;

namespace BabzRent.Controllers
{
    public class ExpensesController : Controller
    {
        private ApplicationDbContext _context { get; set; }

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Expense
        public ActionResult Index()
        {
            var viewModel = new ExpenseViewModel()
            {
                Expenses = _context.Expenses.Include(e => e.Company).Include(e => e.ExpenseLocation).Include(e => e.BillableProperty).Include(e => e.BillableLocation).ToList()
            };

            return View(viewModel);
        }

        public ActionResult NewExpense()
        {
            var viewModel = new ExpenseFormViewModel
            {
                Expense = new Expense(),
                Companies = _context.ExpenseCompanies.ToList(),
                Locations = ExpenseCompanyLocation.GetLocationsSelectList(),
                BillableLocations = _context.PropertyLocations.ToList(),
                BillableProperties = Property.GetPropertySelectList()
            };
            return View("ExpenseForm", viewModel);
        }

        public ActionResult SaveExpense(Expense expense)
        {
            var expenseRepo = new Expense(_context);
            expenseRepo.SaveExpense(expense);

            return RedirectToAction("Index");
        }



        public ActionResult GetCompanyLocations(int companyId)
        {// Used to return a JSON formatted object
            if (companyId != 0)
            {
                var locationRepo = new ExpenseCompanyLocation(_context);
                IEnumerable<SelectListItem> locations = locationRepo.GetLocationsSelectList(companyId);
                return Ok(locations);
            }
            return null;
        }

        public ActionResult GetBillableProperties(int locationId)
        {
            if (locationId != 0)
            {
                var propertyRepo = new Property(_context);
                IEnumerable<SelectListItem> properties = propertyRepo.GetPropertySelectList(locationId);
                return Ok(properties);
            }

            return null;
        }
        


        public ActionResult NewCompany()
        {
            var company = new CompanyFormViewModel()
            {
                Company = new ExpenseCompany(),
                CompanyLocation = new ExpenseCompanyLocation()
            };
            return View("CompanyForm", company);
        }

        public ActionResult SaveCompany(CompanyFormViewModel viewModel)
        {
            var expenseCompanyRepo = new ExpenseCompany(_context);
            expenseCompanyRepo.SaveCompany(viewModel);

            return View("Index");
        }

        public ActionResult EditCompany(int companyId)
        {
            var company = new CompanyFormViewModel()
            {
                Company = _context.ExpenseCompanies.SingleOrDefault(c => c.Id == companyId)
            };
            return View("CompanyForm", company);
        }



        public ActionResult NewCompanyLocation()
        {
            var viewModel = new CompanyLocationViewModel()
            {
                CompanyLocation = new ExpenseCompanyLocation(),
                Companies = _context.ExpenseCompanies.ToList()
            };

            return View("CompanyLocationForm", viewModel);
        }

        public ActionResult SaveCompanyLocation(CompanyLocationViewModel viewModel)
        {
            if(viewModel.CompanyLocation.Id == 0)
            {
                _context.ExpenseCompanyLocations.Add(viewModel.CompanyLocation);
            }
            else
            {
                var locationInDb = _context.ExpenseCompanyLocations.Single(l => l.Id == viewModel.CompanyLocation.Id);
                locationInDb.CompanyId = viewModel.CompanyLocation.CompanyId;
                locationInDb.Address1 = viewModel.CompanyLocation.Address1;
                locationInDb.Address2 = viewModel.CompanyLocation.Address2;
                locationInDb.Address3 = viewModel.CompanyLocation.Address3;
                locationInDb.City = viewModel.CompanyLocation.City;
                locationInDb.PostCode = viewModel.CompanyLocation.PostCode;
                locationInDb.County = viewModel.CompanyLocation.County;
                locationInDb.Country = viewModel.CompanyLocation.Country;
            }
            _context.SaveChanges();
            return View("Index");
        }

        public ActionResult Companies()
        {
            var viewModel = new CompaniesViewModel()
            {
                Companies = _context.ExpenseCompanies.ToList(),
                Locations = _context.ExpenseCompanyLocations.ToList()
            };

            return View(viewModel);

        }
    }
}