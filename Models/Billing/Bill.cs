using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Tenancies;
using BabzRent.Data;
using BabzRent.Models.Maintenance;
using BabzRent.Models.Properties;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabzRent.Models.Billing
{
    public class Bill
    {
        public int Id { get; set; }
        public BillType BillType { get; set; }
        public byte BillTypeId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime PeriodFromDate { get; set; }
        public DateTime PeriodToDate { get; set; }
        public Tenancy Tenancy { get; set; }
        public int? TenancyId { get; set; }
        public Property Property { get; set; }
        public int PropertyId { get; set; }
        public DateTime DateCreated { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public byte InvoiceStatusId { get; set; }
        public Invoice Invoice { get; set; }
        public int? InvoiceId { get; set; }
        public float? PreviousMeterStatus { get; set; }
        public float? MeterStatus { get; set; }
        public float? Usage { get; set; }
        public float? UsageIncluded { get; set; }
        public string UnitType { get; set; }
        public float? UnitCost { get; set; }

        public int? MeterId { get; set; }
        public MeterReading MeterReading { get; set; }
        public int? MeterReadingId { get; set;}

        [NotMapped]
        public ApplicationDbContext _context { get; set; }

        public Bill()
        {

        }

        public Bill(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Bill> GetBillsNotYetInvoiced(int tenancyId)
        {
            List<Bill> bill = new List<Bill>();

            var notInvoicedStatusIds = InvoiceStatus.NotInvoicedStatusIds();
            var outstandingBills = _context.Bills.Where(b => b.TenancyId == tenancyId && notInvoicedStatusIds.Contains(b.InvoiceStatusId)).ToList();
            if (outstandingBills.Count() !=0)
            {
                bill.AddRange(outstandingBills);
            }
            return bill;
        }

        public static double MnthInclBillTypeAmountInContract(Contract lastContract, int BillTypeId)
        {
            float? billAmountIncluded = null;

            switch (BillTypeId)
            {
                case 3:
                    billAmountIncluded = lastContract.ElectricityIncluded.GetValueOrDefault();
                    break;
                case 4:
                    billAmountIncluded = lastContract.GasIncluded.GetValueOrDefault();
                    break;
                case 5:
                    billAmountIncluded = lastContract.WaterIncluded.GetValueOrDefault();
                    break;
                case 6:
                    billAmountIncluded = lastContract.HotWaterIncluded.GetValueOrDefault();
                    break;
                case 7:
                    billAmountIncluded = lastContract.RubbishIncluded.GetValueOrDefault();
                    break;
            }

            return Convert.ToDouble(billAmountIncluded);
        }

        public static double ProRataBillInclUnitAmount(int tenancyId, int billTypeId, DateTime periodStartDate, DateTime periodEndDate, ApplicationDbContext _context)
        {
            double monthlyAmountIncluded;

            Contract lastContract = _context.Contracts.Where(c => c.TenancyId == tenancyId & c.ContractStartDate <= periodEndDate).OrderBy(c => c.ContractEndDate).ToList().LastOrDefault();
            if (lastContract != null)
            {
                monthlyAmountIncluded = MnthInclBillTypeAmountInContract(lastContract, billTypeId);
            }
            else
            {
                monthlyAmountIncluded = 0;
            }
            
            return monthlyAmountIncluded *
                (int)(periodEndDate - periodStartDate).TotalDays / 30;
        }
    }
}