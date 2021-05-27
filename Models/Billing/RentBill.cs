using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Data;

namespace BabzRent.Models.Billing
{
    public class RentBill
    {
        public ApplicationDbContext _context { get; set; }
        public RentBill(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public int DaysThresholdForSingleBill = 10;

        public void InitialBill(int tenancyId)
        {
            var bill = GenerateBill(tenancyId);
            _context.Bills.Add(bill);
            _context.SaveChanges();

            //if number of days for this bill is less than threshold.
            if ((bill.PeriodToDate - bill.PeriodFromDate).Days < DaysThresholdForSingleBill)
            {
                var bill1 = GenerateBill(tenancyId);
                _context.Bills.Add(bill1);
                _context.SaveChanges();
            }

            //Add Deposit Bill to be issued.

            //implement close tenancy function.
        }

        public void CreateBillsAtLocation(int locationId)
        {
            //This function creates a Rent Bill for all tenancies currently active
            var allTenancies = _context.Tenancies.Where(t => 
            t.Property.PropertyLocationId == locationId 
            & (t.MoveOutDate == null || t.MoveOutDate>=DateTime.Now) 
            & (t.MoveInDate <= DateTime.Now)).ToList();

            foreach (var tenancy in allTenancies)
            {
                var bill = GenerateBill(tenancy.Id);
                _context.Bills.Add(bill);
            }
            _context.SaveChanges();
        }

        private Bill GenerateBill(int tenancyId)
        {
            DateTime periodFromDate;
            DateTime periodToDate;
            double amount;
            float usage;

            //this entire function only works on active tenancies... Expired tenancies should not be ran through this function.
            var currentContract = _context.Contracts.Where(c => c.TenancyId == tenancyId).OrderBy(c => c.ContractVersion).ToList().LastOrDefault();
            var previousBills = _context.Bills.Where(b => b.BillTypeId == 1 & b.TenancyId == tenancyId).OrderBy(b => b.PeriodToDate).ToList();
            var currentTenancy = _context.Tenancies.Single(t => t.Id == tenancyId);


            //Set Billing Dates
            if (previousBills.Count != 0)
            {
                periodFromDate = previousBills.LastOrDefault().PeriodToDate;
                periodToDate = periodFromDate.AddMonths(1);

                //Think if possible to amend this routine to take into consideration active bills.
                if (periodToDate >= DateTime.Now.AddDays(37+DaysThresholdForSingleBill))
                {   //introduce a check to make sure no double bill being introduced
                    //This will decline creation of bills too far in advance. Whilst allowing initial bill
                    //to create a second bill, if not meeting the threshold. +7 days added in case tenancy is 
                    //signed 1 week before move in.
                    throw new ApplicationException("Bill is too far in the future.");
                }
                else if (currentTenancy.MoveOutDate != null)
                {
                    if (currentTenancy.MoveOutDate <= DateTime.Now & 
                        (previousBills.LastOrDefault().PeriodToDate == currentTenancy.MoveOutDate))
                    { 
                        //Last bill already produced - Exit 
                        throw new ApplicationException("Last bill already produced");
                    }
                    else if (periodFromDate > currentTenancy.MoveOutDate)
                    {//tenanancy has already finished regenerate a new bill. For previous Bill. Old bill will need to be refunded.
                     //implements the edit function if the tenants moved out prior to their billing date 
                        periodFromDate = previousBills.Last().PeriodFromDate;
                        periodToDate = (DateTime)currentTenancy.MoveOutDate;
                    }
                    else if (periodToDate.Date > currentTenancy.MoveOutDate)
                    { // Amend date to ensure invoiceToDate is not greater than tenancy end date...
                        periodToDate = (DateTime)currentTenancy.MoveOutDate;
                    }
                }
            }
            else
            {
                //First Bill at Move In Date
                periodFromDate = currentTenancy.MoveInDate;
                periodToDate = periodFromDate.AddMonths(1);
                if (periodToDate.Day > currentContract.BillingDate)
                {
                    //Amend the periodToDate to equal the billing date if not already the case.
                    periodToDate = new DateTime(periodToDate.Year, periodToDate.Month, currentContract.BillingDate);
                } 
                else if (periodToDate.Day < currentContract.BillingDate)
                {
                    periodToDate = new DateTime(periodFromDate.Year, periodFromDate.Month, currentContract.BillingDate);
                }
            }

            // ***figure out what will take place if both dates are the same 12/12/20 and 12/12/2020... no bill should be produced****************
            if (periodFromDate.Date == periodToDate.Date)
            {
                throw new ApplicationException();
            }
            else if (periodFromDate.Day != periodToDate.Day)
            {
                //If billing is not for a whole month = Work out pro rata.

                usage = (periodToDate.Day - periodFromDate.Day) / 30;
                amount =  usage * currentContract.PaymentAmount;

            }
            else
            {
                amount = currentContract.PaymentAmount;
                usage = 1;
            }



            var bill = new Bill()
            {
                PeriodToDate = periodToDate,
                PeriodFromDate = periodFromDate,
                Description = _context.BillTypes.Single(i => i.Id == 1).NamePL + ", za okres : " + periodFromDate.ToShortDateString() + " - " + periodToDate.ToShortDateString(),
                Usage = usage,
                BillTypeId = 1,
                DateCreated = DateTime.Now,
                InvoiceStatusId = 0,
                PropertyId = currentTenancy.PropertyId,
                TenancyId = tenancyId,
                Amount = amount,
                UnitType = "miesiac",
                UnitCost = currentContract.PaymentAmount
            };
            return bill;
        }

    }
}