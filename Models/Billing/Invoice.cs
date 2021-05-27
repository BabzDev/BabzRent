using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Tenancies;
using BabzRent.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BabzRent.Models.Billing
{
    public class Invoice
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public Tenancy Tenancy { get; set; }
        public int TenancyId { get; set; }
        public string Reference { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? DateSettled { get; set; }
        public DateTime? DateIssued { get; set; }

        [NotMapped]
        private ApplicationDbContext _context { get; set; }

        public Invoice()
        {

        }

        public Invoice(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public void CreateInvoicesForLocation(int locationId)
        {
            //This function creates Invoices for all tenancies that are active or have been closed within the last 3 months
            var properties = _context.Properties.Where(p => p.PropertyLocationId == locationId).ToList();
            var allTenancies = _context.Tenancies.Where(t => t.Property.PropertyLocationId == locationId).ToList();
            foreach (var property in properties)
            {
                var tenancies = allTenancies.Where(t => t.PropertyId == property.Id && (t.MoveOutDate == null || t.MoveOutDate >= DateTime.Now.AddMonths(-3))).ToList();
                foreach (var tenancy in tenancies)
                {
                    CreateInvoice(tenancy.Id);
                }
            }
        }

        public void CreateInvoice(int tenancyId)
        {
            var notIssuedInvoiceID = GetUnIssuedInvoiceId(tenancyId);
            
            if (notIssuedInvoiceID != 0)
            {
                UpdateUnissuedInvoiceWithNewBills(tenancyId);

            }
            else
            {
                double amount = 0;
                var billRepo = new Bill(_context);
                var outstandingBills = billRepo.GetBillsNotYetInvoiced(tenancyId);

                if (outstandingBills.Count() != 0) //Only create invoices if bills are outstanding
                {
                    var invoice = new Invoice
                    {
                        Amount = amount,
                        DateCreated = DateTime.Now,
                        TenancyId = tenancyId,
                        DueDate = DateTime.Now.AddDays(7)
                    };
                    _context.Invoices.Add(invoice);
                    _context.SaveChanges();

                    foreach (var bill in outstandingBills)
                    {
                        amount = bill.Amount + amount;
                        bill.InvoiceStatusId = InvoiceStatus.PostInvoicingStatusId(bill.InvoiceStatusId);
                        bill.InvoiceId = invoice.Id;
                    }

                    invoice.Amount = amount;
                    _context.SaveChanges();
                }
            }
        }

        public int GetUnIssuedInvoiceId(int tenancyId)
        {
            var unIssuedInvoices = _context.Invoices.Where(i => i.TenancyId == tenancyId && i.DateIssued == null).ToList();

            if (unIssuedInvoices.Count != 0)
            {
                return unIssuedInvoices.LastOrDefault().Id;
            }
            return 0;
        }

        public void UpdateUnissuedInvoiceWithNewBills(int tenancyId)
        {
            var notInvoicedStatusIds = InvoiceStatus.NotInvoicedStatusIds();
            var outstandingBills = _context.Bills.Where(b => b.TenancyId == tenancyId && notInvoicedStatusIds.Contains(b.InvoiceStatusId)).ToList();
            var invoice = _context.Invoices.Single(i => i.TenancyId == tenancyId && i.DateIssued == null);
            double amount = 0;

            if (outstandingBills.Count != 0)
            {
                foreach (var bill in outstandingBills)
                {
                    amount = bill.Amount + amount;
                    bill.InvoiceStatusId = InvoiceStatus.PostInvoicingStatusId(bill.InvoiceStatusId);
                    bill.InvoiceId = invoice.Id;
                }
                invoice.DateCreated = DateTime.Now;
                invoice.DueDate = DateTime.Now.AddDays(7);
                invoice.Amount += amount;
                _context.SaveChanges();
            }
        }

        public void DeleteInvoice(int invoiceId)
        {
            var bills = _context.Bills.Where(b => b.InvoiceId == invoiceId).ToList();
            var invoice = _context.Invoices.Single(i => i.Id == invoiceId);

            if (bills.Count() != 0)
            {
                foreach(var bill in bills)
                {
                    bill.InvoiceStatusId = 0;
                    bill.InvoiceId = null;
                    _context.SaveChanges();
                }
            }
            _context.Invoices.Remove(invoice);
            _context.SaveChanges();
        }

        public IEnumerable<Invoice> GetAllInvoicesAtLocation(int locationId)
        {
            var invoices = _context.Invoices
                .Where(i => i.Tenancy.Property.PropertyLocationId == locationId & i.DateIssued != null)
                .Include(i => i.Tenancy.Tenant)
                .ToList();
            return invoices;
        }

        public IEnumerable<Invoice> GetAllInvoicesAtLocationForTaxYear(int locationId, int taxYear)
        {
            var startTaxYearDate = new DateTime(taxYear, 1, 1);
            var endTaxYearDate = new DateTime(taxYear, 12, 31);
            var invoices = _context.Invoices
                .Where(i => i.DateIssued >= startTaxYearDate & i.DateIssued <= endTaxYearDate & i.Tenancy.Property.PropertyLocationId == locationId & i.DateIssued != null)
                .Include(i => i.Tenancy.Tenant)
                .ToList();
            return invoices;
        }

        public void Issue(int invoiceId)
        {
            var invoice = _context.Invoices.Include(i => i.Tenancy.Property).Single(i => i.Id == invoiceId);
            invoice.Reference = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + invoice.Tenancy.Property.PropertyName;
            var referenceLength = invoice.Reference.Length;

            var alreadyIssuedInvoices = _context.Invoices.Where(
                i => i.Tenancy.PropertyId == invoice.Tenancy.PropertyId 
                && i.DateIssued != null).ToList();

            if (alreadyIssuedInvoices.Count() != 0)
            {
                alreadyIssuedInvoices = alreadyIssuedInvoices.Where(i => i.DateIssued.GetValueOrDefault().Year == DateTime.Now.Year & i.DateIssued.GetValueOrDefault().Month == DateTime.Now.Month).ToList();

                if (alreadyIssuedInvoices.Count() != 0)
                {
                    invoice.Reference = invoice.Reference + "/" + alreadyIssuedInvoices.Count();
                }
            }

            invoice.DateIssued = DateTime.Now;
            _context.SaveChanges();
        }

        public void Settle(int invoiceId)
        {
/*            var propertyName = _context.Tenancies.Include(t => t.Property).Single(t => t.Id == tenancyId).Property.PropertyName;
            var currentInvoice = _context.Invoices.Include(i => i.Tenancy.Property).Single(i => i.Id == invoiceId);
            var invoicesAtPropertyThisMonth = new List<Invoice>();


            var invoicesAtProperty = _context.Invoices.Where(i => i.Tenancy.PropertyId == currentInvoice.Tenancy.PropertyId && DateIssued != null).ToList();
            if (invoicesAtProperty.Count() != 0)
            {
                invoicesAtPropertyThisMonth = invoicesAtProperty.Where(i =>
                    i.DateIssued.GetValueOrDefault().Year == DateTime.Now.Year
                    && i.DateIssued.GetValueOrDefault().Month == DateTime.Now.Month).ToList();
            }

            var invoicesForTenancy = _context.Invoices.Where(i => i.TenancyId == currentInvoice.TenancyId && i.DateSettled != null).ToList();


            var issuedInvoices = _context.Invoices.Where(i => i.Tenancy.Property)
            var issuedInvoicesThisMonth = _context.Invoices.Where(i => i.DateIssued.GetValueOrDefault().Month == DateTime.Now.Month && i.DateIssued.GetValueOrDefault().Year == DateTime.Now.Year).ToList();

            if (issuedInvoicesThisMonth.Count() != 0)
            {
                invoiceVersion += issuedInvoicesThisMonth.Count();
            }


            currentInvoice.DateSettled = DateTime.Now;
            currentInvoice.Reference = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + currentInvoice.Tenancy.Property.PropertyName + "-" +  */
        }
    }
}