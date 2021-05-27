using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models;
using BabzRent.Models.Tenancies;
using BabzRent.Models.Maintenance;
using BabzRent.Data;

namespace BabzRent.Models.Billing
{
    public class MeteredBill
    {
        private ApplicationDbContext _context { get; set; }
        public MeteredBill(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public void UpdateBillAndMeterReading(MeterReading changedMeterReading)
        {
            int propertyId = _context.Meters.SingleOrDefault(m => m.Id == changedMeterReading.MeterId).PropertyId;
            byte billTypeId = _context.Meters.Single(m => m.Id == changedMeterReading.MeterId).BillTypeId;
            bool overrideUnits = true;
            var bill1 = _context.Bills.Single(m => m.MeterReadingId == changedMeterReading.Id);
            var updatedBill1 = GenerateBill(propertyId, billTypeId, changedMeterReading);

            var dbMeterReading = _context.MeterReadings.Single(m => m.Id == changedMeterReading.Id);
            //Manuall mapping update to Meter Reading in database
            {
                dbMeterReading.DateCreated = DateTime.Now;
                dbMeterReading.ReadingDate = changedMeterReading.ReadingDate;
                dbMeterReading.Status = changedMeterReading.Status;
            }

            var utilityCostRepo = new UtilityCosts(_context);
            var currentCosts = utilityCostRepo.GetCurrentCosts(bill1.PeriodFromDate, propertyId, billTypeId);

            if (currentCosts.FixedCost1 == 0)
            {
                overrideUnits = false;
            }

            //Manually mapping update to first bill in database
            {
                bill1.DateCreated = DateTime.Now;
                bill1.Description = updatedBill1.Description;
                bill1.Usage = overrideUnits == false ? updatedBill1.Usage : 1;
                bill1.UnitType = overrideUnits == false ? _context.BillTypes.Single(b => b.Id == billTypeId).UnitTypePL : "N/A";
                bill1.UnitCost = overrideUnits == false ? currentCosts.VariableCost1 : (float)updatedBill1.Amount;
                bill1.UsageIncluded = updatedBill1.UsageIncluded;
                bill1.InvoiceStatusId = updatedBill1.InvoiceStatusId;
                bill1.PeriodFromDate = updatedBill1.PeriodFromDate;
                bill1.PeriodToDate = updatedBill1.PeriodToDate;
                bill1.Amount = updatedBill1.Amount;
                bill1.PreviousMeterStatus = updatedBill1.PreviousMeterStatus;
                bill1.MeterStatus = updatedBill1.MeterStatus;
            }

            _context.SaveChanges();

            var bill2 = _context.Bills.Where(b => b.PropertyId == propertyId
                & b.BillTypeId == billTypeId & b.PeriodFromDate >= changedMeterReading.ReadingDate)
                .OrderBy(b => b.PeriodToDate).FirstOrDefault();
            if (bill2 != null)
            {
                var futureCosts = utilityCostRepo.GetCurrentCosts(bill2.PeriodFromDate, propertyId, billTypeId);
                if (futureCosts.FixedCost1 == 0)
                {
                    overrideUnits = false;
                }
                var futureBillMeterReading = _context.MeterReadings.Single(m => m.Id == bill2.MeterReadingId);
                var updatedBill2 = GenerateBill(propertyId, billTypeId, futureBillMeterReading);
                //Manually mapping update to first bill in database
                {
                    bill2.DateCreated = DateTime.Now;
                    bill2.Description = updatedBill2.Description;
                    bill2.Usage = overrideUnits == false ? updatedBill2.Usage : 1;
                    bill2.UnitType = overrideUnits == false ? _context.BillTypes.Single(b => b.Id == billTypeId).UnitTypePL : "N/A";
                    bill2.UnitCost = overrideUnits == false ? futureCosts.VariableCost1 : (float)updatedBill2.Amount;
                    bill2.UsageIncluded = updatedBill2.UsageIncluded;
                    bill2.InvoiceStatusId = updatedBill2.InvoiceStatusId;
                    bill2.PeriodFromDate = updatedBill2.PeriodFromDate;
                    bill2.PeriodToDate = updatedBill2.PeriodToDate;
                    bill2.Amount = updatedBill2.Amount;
                    bill2.PreviousMeterStatus = updatedBill2.PreviousMeterStatus;
                    bill2.MeterStatus = updatedBill2.MeterStatus;
                }
                _context.SaveChanges();
            }

        }

        public void SaveNewBillAndMeterReading(MeterReading meterReading)
        {
            int propertyId = _context.Meters.SingleOrDefault(m => m.Id == meterReading.MeterId).PropertyId;
            byte billTypeId = _context.Meters.Single(m => m.Id == meterReading.MeterId).BillTypeId;

            var bill = GenerateBill(propertyId, billTypeId, meterReading);

            _context.MeterReadings.Add(meterReading);
            _context.SaveChanges();

            bill.MeterReadingId = meterReading.Id;
            _context.Bills.Add(bill);
            _context.SaveChanges();

        }

        public Bill GenerateBill(int propertyId, byte billTypeId, MeterReading meterReading)
        {
            float newMeterStatus = meterReading.Status;
            double billAmount;
            byte invoiceStatus;
            float previousMeterStatus;
            double meterUsagePostAllowance;
            int? tenancyId = null;
            DateTime billingPeriodFromDate;
            Bill previousBill;
            bool overrideUnits = true;

            TenancyRepository tenanciesInDb = new TenancyRepository(_context);
            var tenancy = tenanciesInDb.GetActiveTenancyAtDateForProperty(propertyId, meterReading.ReadingDate);
            if (tenancy != null)
            {
                tenancyId = tenancy.Id;
            }

            (previousBill, invoiceStatus)= GetPreviousBill(meterReading, billTypeId, tenancy);

            (previousMeterStatus, billingPeriodFromDate, invoiceStatus) = GetPreviousMeterValues(previousBill, meterReading.ReadingDate, meterReading.MeterId, invoiceStatus);

            double proRataAmountIncluded = 
                Bill.ProRataBillInclUnitAmount(Convert.ToInt32(tenancyId), billTypeId, billingPeriodFromDate, meterReading.ReadingDate, _context);

            meterUsagePostAllowance = Math.Max(((float)newMeterStatus - previousMeterStatus - (float)proRataAmountIncluded),0);

            billAmount = CalculateBillAmount(meterUsagePostAllowance, billingPeriodFromDate, meterReading.ReadingDate, propertyId, billTypeId);


            var utilityCostRepo = new UtilityCosts(_context);
            var currentCosts = utilityCostRepo.GetCurrentCosts(billingPeriodFromDate, propertyId, billTypeId);
            
            if (currentCosts.FixedCost1 == 0)
            {
                overrideUnits = false;
            }

            var bill = new Bill()
            {
                TenancyId = tenancyId,
                PropertyId = propertyId,
                BillTypeId = billTypeId,
                DateCreated = DateTime.Now.Date,
                InvoiceStatusId = invoiceStatus,
                PeriodFromDate = billingPeriodFromDate,
                PeriodToDate = meterReading.ReadingDate,
                Amount = billAmount,
                UsageIncluded = (float)proRataAmountIncluded,
                Description = _context.BillTypes.Single(b => b.Id == billTypeId).NamePL,
                PreviousMeterStatus = previousMeterStatus,
                MeterStatus = newMeterStatus,
                MeterId = meterReading.MeterId,
                MeterReadingId = meterReading.Id,
                Usage = overrideUnits == false ? (float)meterUsagePostAllowance : 1,
                UnitType = overrideUnits == false ? _context.BillTypes.Single(b => b.Id == billTypeId).UnitTypePL : "N/A",
                UnitCost = overrideUnits == false ? currentCosts.VariableCost1 : (float)billAmount
            };

            return bill;
        }

        private (Bill PreviousBill, byte InvoiceStatus) GetPreviousBill(MeterReading meterReading, int billTypeId, Tenancy tenancy)
        {
            int indexOfPreviousBill = 0;
            Bill previousBill = null;
            byte invoiceStatus = 0;

            if (tenancy != null)
            {
                var bills = _context.Bills.Where(m => m.TenancyId == tenancy.Id & m.BillTypeId == billTypeId).OrderBy(b => b.PeriodToDate).ToList();

                if (bills.Count != 0)
                //Check if any bill exists otehrwise return defautl of null
                {
                    if (!(meterReading.Id != 0 & bills.Count == 1))
                    //If we are editing current Meter reading and only 1 bill exists = No previous bill exists. Returns null
                    //Otherwise find current bill
                    {
                        if (meterReading.Id != 0 & bills.Count >= 2)
                            indexOfPreviousBill = bills.FindIndex(b => b.MeterReadingId == meterReading.Id) - 1;
                        else if (meterReading.Id == 0 & bills.Count >= 1)
                            indexOfPreviousBill = bills.Count - 1;

                        previousBill = bills[indexOfPreviousBill];
                    }
                }
            }
            else
                invoiceStatus = 6; //Sets Invoice Status to Maintenance Cost if no previous tenancy exists


            return (previousBill, invoiceStatus);
        }

        private (float PreviousMeterStatus, DateTime PreviousBillingPeriodToDate, byte InvoiceStatus) GetPreviousMeterValues(Bill previousBill, DateTime meterReadingDate ,int meterId, byte invoiceStatus)
        {
            float previousMeterStatus;
            DateTime periodToDate;


            if (previousBill != null)
            // If previous bill exists, take previous meter status from last bill and set previous bills date.
            {
                previousMeterStatus = (float) previousBill.MeterStatus;
                periodToDate = previousBill.PeriodToDate;
            }
            else
            // If no previous bill exists, take previous meter status and status date.
            {
                MeterReading previousMeterReading = _context.MeterReadings.Where(m => m.MeterId == meterId).OrderBy(M => M.ReadingDate).ToList().LastOrDefault();

                if (previousMeterReading != null)
                {
                    previousMeterStatus = (float) previousMeterReading.Status;
                    periodToDate = _context.MeterReadings.Where(m => m.MeterId == meterId).OrderByDescending(m => m.ReadingDate).ToList().LastOrDefault().ReadingDate.AddDays(1);

                }
                else
                //This part of routine will only be hit when inital MeterReading is being created and there are no Previous Meter Readings.
                { 
                    previousMeterStatus = 0;
                    invoiceStatus = 11;
                    periodToDate = meterReadingDate.AddDays(-1);
                }

            }
            return (previousMeterStatus, periodToDate, invoiceStatus);
        }
        
        private double CalculateBillAmount(double meterUsage, DateTime billingPeriodFromDate, DateTime billingPeriodToDate, int propertyId, int billTypeId)
        {
            var utilityCostRepo = new UtilityCosts(_context);
            var currentCosts = utilityCostRepo.GetCurrentCosts(billingPeriodFromDate, propertyId, billTypeId);

            return meterUsage * currentCosts.VariableCost1 * currentCosts.VariableCost2
                + (currentCosts.FixedCost1 + currentCosts.FixedCost2) * (int)(billingPeriodToDate - billingPeriodFromDate).TotalDays / 30;
        }
    }
}   