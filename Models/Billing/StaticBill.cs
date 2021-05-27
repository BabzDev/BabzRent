using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Data;

namespace BabzRent.Models.Billing
{
    public class StaticBill
    {
        public ApplicationDbContext _context { get; set; }
        public StaticBill(ApplicationDbContext context)
        {
            _context = context;
        }

        public void EditBill(Bill bill)
        {
            var notInvoicedStatusIds =  InvoiceStatus.NotInvoicedStatusIds();
            if (notInvoicedStatusIds.Contains(bill.InvoiceStatusId))
            {
                var billInDb = _context.Bills.Single(b => b.Id == bill.Id);
                billInDb.Description = bill.Description;
                billInDb.InvoiceStatusId = bill.InvoiceStatusId;
                billInDb.PropertyId = bill.PropertyId;
                billInDb.TenancyId = bill.TenancyId;
                billInDb.BillTypeId = bill.BillTypeId;
                billInDb.PeriodFromDate = bill.PeriodFromDate;
                billInDb.PeriodToDate = bill.PeriodToDate;
                billInDb.Amount = bill.Amount;
                billInDb.MeterStatus = bill.MeterStatus;
                billInDb.DateCreated = DateTime.Now;
                billInDb.Usage = bill.Usage;
                billInDb.UnitType = bill.UnitType;
                billInDb.UnitCost = bill.UnitCost;
                _context.SaveChanges();
            }
        }

        public void AddNewBill(Bill bill)
        {
            bill = GenerateBill(bill.Amount, bill.BillTypeId, bill.Description, bill.PeriodFromDate, bill.PeriodToDate, bill.PropertyId, bill.Usage, bill.UnitCost, bill.UnitType, bill.TenancyId, bill.InvoiceStatusId);
            _context.Bills.Add(bill);
            _context.SaveChanges();
        }

        private Bill GenerateBill(double billAmount, byte billTypeId, string Description, DateTime periodFromDate, DateTime periodToDate, 
            int propertyId, float? usage, float? unitCost, string unitType, int? tenancyid, byte? invoiceStatusId)
        {
            bool overrideUnits = true;

            if (invoiceStatusId == null)
            {
                invoiceStatusId = 0;
            }
            
            if(Description == null)
            {
                Description = _context.BillTypes.Single(i => i.Id == billTypeId).NamePL + ", za okres : " + periodFromDate.ToShortDateString() + " - " + periodToDate.ToShortDateString();
            }

            if(usage != null & unitCost != null)
            {
                overrideUnits = false;
                if (unitType == null)
                {
                    unitType = _context.BillTypes.Single(b => b.Id == billTypeId).UnitTypePL;
                }
            }
            else
            {
                if (unitType == null)
                {
                    unitType = "N/A";
                }
            }



            var bill = new Bill()
            {
                TenancyId = tenancyid,
                Amount = billAmount,
                Description = Description,
                BillTypeId = billTypeId,
                PeriodFromDate = periodFromDate,
                PeriodToDate = periodToDate,
                PropertyId = propertyId,
                DateCreated = DateTime.Now,
                InvoiceStatusId = (byte)invoiceStatusId,
                Usage = overrideUnits!=true ? usage.GetValueOrDefault() : 1,
                UnitCost = overrideUnits!=true ? unitCost.GetValueOrDefault() : (float) billAmount,
                UnitType = unitType
            };
            return bill;
        }
    }
}