using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabzRent.Data;
using BabzRent.Models.Maintenance;
using BabzRent.Models;
using BabzRent.ViewModels.MeterViewModels;
using BabzRent.Models.Billing;
using BabzRent.Models.Tenancies;
using BabzRent.ViewModels.Maintenance;
using BabzRent.Models.Properties;

namespace BabzRent.Controllers
{
    public class MaintenanceController : Controller
    {
        public ApplicationDbContext _context { get; set; }

        public MaintenanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Meter
        public ActionResult Index()
        {
            var meteredBillTypeIds = BillType.GetMeteredBillTypeIds();
            var viewModel = new MaintenanceIndexViewModel()
            {
                Locations = _context.PropertyLocations.ToList(),
                BillTypes = _context.BillTypes.Where(b => meteredBillTypeIds.Contains(b.Id)).ToList()
            };

            return View(viewModel);
        }

        public ActionResult Meters()
        {
            var viewModel = new MeterIndexViewModel()
            {
                Meter = _context.Meters.Include(m => m.Property).Include(m => m.BillType).ToList(),
                MeterReadings = _context.MeterReadings.ToList(),
                Locations = _context.PropertyLocations.ToList()
            };
            return View(viewModel);
        }


        public ActionResult MeterDetails(int id)
        {
            var viewModel = new MeterDetailsViewModel()
            {
                Meter = _context.Meters.Single(m => m.Id == id),
                MeterReadings = _context.MeterReadings.Where(m => m.MeterId == id)
            };

            return View(viewModel);
        }

        public ActionResult NewMeter(int? propertyId)
        {
            var properties = _context.Properties.ToList();

            if (propertyId != null)
            {
                properties = properties.Where(m => m.Id == (int)propertyId).ToList();
            }

            var viewModel = new MeterFormViewModel()
            {
                Meter = new Meter(),
                Properties = properties,
                BillTypes = _context.BillTypes.ToList()
            };

            return View("MeterForm", viewModel);
        }

        public ActionResult EditMeter(int id)
        {
            var viewModel = new MeterFormViewModel()
            {
                Meter = _context.Meters.SingleOrDefault(m => m.Id == id),
                Properties = _context.Properties.ToList(),
                BillTypes = _context.BillTypes.ToList()
            };

            return View("MeterForm", viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SaveMeter(MeterFormViewModel viewModel)
        {
            if (viewModel.Meter.Id == 0)
            {
                _context.Meters.Add(viewModel.Meter);

                UtilityCosts newUtilityCost = new UtilityCosts
                {
                    BillTypeId = viewModel.Meter.BillTypeId,
                    PropertyId = viewModel.Meter.PropertyId,
                    DateEffectiveFrom = viewModel.UtilityCosts.DateEffectiveFrom,
                    DateEffectiveExpiry = viewModel.UtilityCosts.DateEffectiveExpiry,
                    FixedCost1 = viewModel.UtilityCosts.FixedCost1,
                    FixedCost2 = viewModel.UtilityCosts.FixedCost2,
                    VariableCost1 = viewModel.UtilityCosts.VariableCost1,
                    VariableCost2 = viewModel.UtilityCosts.VariableCost2,
                    DateUpdated = DateTime.Now
                };
                _context.UtilityCosts.Add(newUtilityCost);
            }
            else
            {
                var meterInDB = _context.Meters.Single(m => m.Id == viewModel.Meter.Id);
                meterInDB.BillTypeId = viewModel.Meter.BillTypeId;
                meterInDB.PropertyId = viewModel.Meter.PropertyId;
            }

            _context.SaveChanges();

            return RedirectToAction("Meters", "Maintenance");
        }

        public ActionResult NewMeterReading(int? id)
        {
            float latestMeterReading = 0;
            var meterId = 0;

            if (id != null)
            {
                var meterReadingInDb = _context.MeterReadings.Where(m => m.MeterId == (int)id).ToList();
                meterId = (int)id;

                if (meterReadingInDb.Count != 0)
                {
                    latestMeterReading = meterReadingInDb.LastOrDefault().Status;
                }
            };

            var viewModel = new MeterReadingFormViewModel()
            {
                MeterReading = new MeterReading() { Status = latestMeterReading, MeterId = meterId, ReadingDate = DateTime.Now },
                Properties = _context.Properties.ToList(),
                Meters = MeterNames.GetList(_context.Meters.Include(m => m.Property).Include(m => m.BillType).ToList())
            };

            return View("MeterReadingForm", viewModel);
        }

        public ActionResult EditMeterReading(int id)
        {
            var meterReading = _context.MeterReadings.Include(m => m.Meter).Single(m => m.Id == id);

            var viewModel = new MeterReadingFormViewModel()
            {
                MeterReading = meterReading,
                Properties = _context.Properties.Where(m => m.Id == meterReading.Meter.PropertyId).ToList(),
                Meters = MeterNames.GetList(_context.Meters.Where(m => m.Id == meterReading.MeterId).Include(m => m.Property).Include(m => m.BillType).ToList())
            };

            return View("MeterReadingForm", viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SaveMeterReading(MeterReading meterReading)
        {
            meterReading.DateCreated = DateTime.Now;
            MeteredBill meteredBill = new MeteredBill(_context);
            if (meterReading.Id != 0)
                meteredBill.UpdateBillAndMeterReading(meterReading);
            else
                meteredBill.SaveNewBillAndMeterReading(meterReading);

            return RedirectToAction("Meters");
        }

        public ActionResult NewMultiMeterReading(int? LocationId, int? BillTypeId)
        {
            var properties = new List<Property>();
            var billTypeIds = new List<int>();
            if (BillTypeId == null)
            {
                billTypeIds = BillType.GetMeteredBillTypeIds();
            }
            else
            {
                billTypeIds.Add((int)BillTypeId);
            }

            if( LocationId == null)
            {
                properties = _context.Properties.ToList();
            }
            else
            {
                properties = _context.Properties.Where(p => p.PropertyLocationId == LocationId).ToList();
            }


            var viewModel = new MultiReadingFormViewModel
            {
                BillTypes = _context.BillTypes.Where(m => billTypeIds.Contains(m.Id)).ToList(),
                MeterReadings = new List<MeterReading>(),
                Meters = _context.Meters.ToList(),
                Properties = properties,
                CurrentMeterReadings = _context.MeterReadings.OrderBy(m => m.ReadingDate).ToList()
            };
            return View("MultiMeterReadingForm", viewModel);
        }

        public ActionResult SaveMultiMeterReading(IEnumerable<MeterReading> meterReadings)
        {
            foreach (var reading in meterReadings)
            {
                MeteredBill meteredBill = new MeteredBill(_context);
                meteredBill.SaveNewBillAndMeterReading(reading);
            }
            return RedirectToAction("Index");
        }
    }
}