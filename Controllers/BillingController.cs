using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models;
using BabzRent.Models.Billing;
using BabzRent.ViewModels.BillingViewModels;
using BabzRent.Models.Tenancies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabzRent.Data;
using BabzRent.Models.Tenants;

namespace BabzRent.Controllers
{
    public class BillingController : Controller
    {
        public ApplicationDbContext _context { get; set; }

        public BillingController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //*******************************    Bills    **************************
        // GET: Payments

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Bills()
        {
            var NotInvoicedBillId = InvoiceStatus.NotInvoicedStatusIds();
            var viewModel = new BillsIndexViewModel()
            {
                Bills = _context.Bills.Include(m => m.Property).Include(m => m.BillType)
                    .Include(m => m.InvoiceStatus).Where(b => NotInvoicedBillId.Contains(b.InvoiceStatusId)).ToList(),
                TenancyNames = TenancyNames.Get(_context.Tenancies.Include(m => m.Property).ToList()),
                Properties = _context.Properties.ToList(),
                PropertyLocations = _context.PropertyLocations.ToList()
            };
            ViewBag.Title = "Current Bills";
            return View("Bills", viewModel);
        }

        public ActionResult BillsArchive()
        {
            var viewModel = new BillsIndexViewModel()
            {
                Bills = _context.Bills.Include(m => m.Property).Include(m => m.BillType)
                    .Include(m => m.InvoiceStatus).ToList(),
                TenancyNames = TenancyNames.Get(_context.Tenancies.Include(m => m.Property).ToList()),
                Properties = _context.Properties.ToList(),
                PropertyLocations = _context.PropertyLocations.ToList()
            };
            ViewBag.Title = "Archive";
            return View("Bills", viewModel);
        }






        //*******************************    Utility Costs    **************************

        public ActionResult UtilityBillCosts()
        {
            var viewModel = new UtilityCostIndexViewModel
            {
                Properties = _context.Properties.ToList(),
                PropertyLocations = _context.PropertyLocations.ToList(),
                UtilityCosts = _context.UtilityCosts.Include(c => c.BillType).ToList()
            };

            return View(viewModel);
        }

        public ActionResult UpdateAllUtilityCostsForLocationForm()
        {
            UpdateAllUtilityCostsForLocationFormViewModel viewModel = new UpdateAllUtilityCostsForLocationFormViewModel()
            {
                PropertyLocations = _context.PropertyLocations.ToList(),
                BillTypes = _context.BillTypes.ToList(),
                UtilityCostsUpdate = new UtilityCostsUpdate()
                {
                    DateEffectiveFrom = DateTime.Now,
                    DateUpdated = DateTime.Now
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UpdateAllUtilityCostsForLocationSave(UtilityCostsUpdate UtilityCostsUpdate)
        {

            UtilityCostsCalculations utilityCalculations = new UtilityCostsCalculations(_context);
            utilityCalculations.UpdateAllCostsAtLocation(UtilityCostsUpdate);

            return RedirectToAction("UtilityBillCosts");
        }






        //*******************************    Invoice    **************************

        public ActionResult Invoice()
        {
            var viewModel = new InvoiceViewModel
            {
                Invoices = _context.Invoices.Where(i => i.DateSettled == null).Include(i => i.Tenancy).ToList(),
                Properties = _context.Properties.ToList(),
                Locations = _context.PropertyLocations.ToList(),
                Tenancies = TenancyNames.Get(_context.Tenancies.Include(m => m.Property).ToList())
            };
            ViewBag.Title = "Not invoiced";
            return View(viewModel);
        }

        public ActionResult InvoiceArchvie()
        {
            var viewModel = new InvoiceViewModel
            {
                Invoices = _context.Invoices.Where(i => i.DateSettled != null).Include(i => i.Tenancy).ToList(),
                Properties = _context.Properties.ToList(),
                Locations = _context.PropertyLocations.ToList(),
                Tenancies = TenancyNames.Get(_context.Tenancies.Include(m => m.Property).ToList())
            };
            ViewBag.Title = "Archive";

            return View("Invoice", viewModel);
        }
        
        public ActionResult CreateInvoiceForm()
        {
            var tenancies = _context.Tenancies.Include(t => t.Property).OrderBy(t => t.MoveOutDate).ToList();
            tenancies = TenancyRepository.ActiveTenanciesPlus3MonthsBack(tenancies);
            var viewModel = new CreateInvoiceFormViewModel()
            {
                Tenancies = TenancyNames.Get(tenancies)
            };
            return View(viewModel);
        }

        public ActionResult CreateInvoice(int tenancyId)
        {
            var invoiceManagement = new Invoice(_context);
            invoiceManagement.CreateInvoice(tenancyId);
            return RedirectToAction("Index", "Billing");
        }

        public ActionResult ViewInvoice(int id)
        {
            var invoice = _context.Invoices.Where(i => i.Id == id).Single();

            var viewModel = new ViewInvoiceViewModel()
            {
                Bills = _context.Bills.Where(m => m.InvoiceId == id).Include(m => m.BillType)
                    .Include(m => m.InvoiceStatus).ToList(),
                Invoice = invoice,
                Tenancy = TenancyNames.Get(_context.Tenancies.Include(m => m.Property).ToList()).Single(t => t.Id == invoice.TenancyId),
                EditStatus = false
            };

            return View(viewModel);
        }

        public ActionResult InvoicesForLocationForm()
        {
            var viewModel = new LocationFormViewModel()
            {
                Locations = _context.PropertyLocations.ToList()
            };
            return View(viewModel);
        }

        public ActionResult CreateInvoicesForLocation(int locationId)
        {
            var invoiceManagement = new Invoice(_context);
            invoiceManagement.CreateInvoicesForLocation(locationId);
            return RedirectToAction("Invoice", "Billing");
        }

        public ActionResult EditInvoice(int id)
        {
            var invoice = _context.Invoices.Where(i => i.Id == id).Single();

            var viewModel = new ViewInvoiceViewModel()
            {
                Bills = _context.Bills.Where(m => m.InvoiceId == id).Include(m => m.BillType)
                    .Include(m => m.InvoiceStatus).ToList(),
                Invoice = invoice,
                Tenancy = TenancyNames.Get(_context.Tenancies.Include(m => m.Property).ToList()).Single(t => t.Id == invoice.TenancyId),
                EditStatus = true
            };

            return View("ViewInvoice", viewModel);
        }

        public ActionResult DeleteBillFromInvoice(int invoiceId, int billId)
        {
            var bill = _context.Bills.Single(b => b.Id == billId);
            bill.InvoiceId = null;
            bill.InvoiceStatusId = 0; //Make sure this function is adaptable. Pull this from InvoiceStatus pre/post invoicing.

            var invoice = _context.Invoices.Single(i => i.Id == invoiceId);
            invoice.Amount = invoice.Amount - bill.Amount;
            _context.SaveChanges();
            
            return RedirectToAction("ViewInvoice", new { id = invoiceId});
        }

        public ActionResult DeleteInvoice(int invoiceId)
        {
            var invoice = new Invoice(_context);
            invoice.DeleteInvoice(invoiceId);

            return RedirectToAction("Invoice");
        }

        public ActionResult IssueInvoice(int invoiceId)
        {
            var invoice = new Invoice(_context);
            invoice.Issue(invoiceId);
            return RedirectToAction("Invoice");
        }

        public ActionResult InvoicePreview(int invoiceId)
        {
            var invoice = _context.Invoices.Single(i => i.Id == invoiceId);
            var tenancyRepo = new Tenancy(_context);
            var tenants = tenancyRepo.GetTenants(invoice.TenancyId);

            var viewModel = new InvoicePreviewViewModel()
            {
                Invoice = invoice,
                Bills = _context.Bills.Where(b => b.TenancyId == invoice.TenancyId).ToList(),
                Tenants = tenants
            };
            return View(viewModel);
        }




        //*******************************    PDF    **************************

        public ActionResult SavePdf(object sender, EventArgs e)
        {
            return View();
        }
















        //*******************************    Rent Bill    **************************

        public ActionResult CreateRentBillsAtLocationForm()
        {
            var viewModel = new LocationFormViewModel()
            {
                Locations = _context.PropertyLocations.ToList()
            };
            return View(viewModel);
        }

        public ActionResult CreateRentBillsAtLocation(int locationId)
        {
            RentBill rentBill = new RentBill(_context);
            rentBill.CreateBillsAtLocation(locationId);
            return RedirectToAction("Bills");
        }


        



        //*******************************    Static Bill    **************************

        public ActionResult StaticBillForm()
        {
            var viewModel = new BillFormViewModel()
            {
                BillTypes = _context.BillTypes.ToList(),
                Properties = _context.Properties.ToList(),
                Tenancies = TenancyNames.Get(_context.Tenancies.Include(t => t.Property).ToList())
            };
            return View(viewModel);
        }

        public ActionResult SaveStaticBill(Bill bill)
        {
            var staticNewBill = new StaticBill(_context);

            if (bill.Id == 0)
                staticNewBill.AddNewBill(bill);
            else
                staticNewBill.EditBill(bill);
            return RedirectToAction("Bills");
        }


        //*******************************   Bill Functions    **************************
        public ActionResult EditBill(int billId)
        {
            var viewModel = new BillFormViewModel()
            {
                BillTypes = _context.BillTypes.ToList(),
                Properties = _context.Properties.ToList(),
                Tenancies = TenancyNames.Get(_context.Tenancies.Include(t => t.Property).ToList()),
                Bill =_context.Bills.Single(m => m.Id == billId)
            };
            return View("StaticBillForm", viewModel);
        }

        public ActionResult DeleteBill(int billId)
        {
            var bill = _context.Bills.Single(b => b.Id == billId);
            _context.Bills.Remove(bill);
            _context.SaveChanges();
            return RedirectToAction("Bills");
        }
    }
}