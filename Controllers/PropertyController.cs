using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabzRent.Data;
using BabzRent.Models;
using BabzRent.Models.Properties;
using BabzRent.ViewModels;
using BabzRent.ViewModels.PropertyViewModels;

namespace BabzRent.Controllers
{
    public class PropertyController : Controller
    {
        private ApplicationDbContext _context { get; set; }

        public PropertyController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Property
        public ActionResult Index()
        {
            var viewModel = new PropertyIndexViewModel()
            {
                Properties = _context.Properties.Include(p => p.PropertyType).Include(p => p.PropertyLocation).ToList(),
                PropertyLocations = _context.PropertyLocations.ToList()
            };

            return View(viewModel);
        }

        public ActionResult PropertyDetails(int id)
        {
            var viewModel = new PropertyDetailsViewModel()
            {
                Property = _context.Properties.Include(m => m.PropertyLocation).Include(m => m.PropertyType).SingleOrDefault(m => m.Id == id),
                Meters = _context.Meters.Include(m => m.BillType).Where(m => m.PropertyId == id).ToList(),
                MeterReadings = _context.MeterReadings.Include(m => m.Meter).Where(m => m.Meter.PropertyId == id).ToList(),
                Tenancies = _context.Tenancies.Where(m => m.PropertyId == id).ToList(),
                Tenants = _context.Tenants.ToList(),
                Contracts = _context.Contracts.ToList()
            };
 
            return View(viewModel);
        }

        public ActionResult Locations()
        {
            var propertyLocations = _context.PropertyLocations.ToList();
            return View(propertyLocations);
        }

        public ActionResult NewLocation()
        {
            var propertyLocation = new PropertyLocation();

            return View("LocationForm", propertyLocation);
        }

        public ActionResult EditLocation(int id)
        {
            var propertyLocation = _context.PropertyLocations.SingleOrDefault(m => m.Id == id);
            return View("LocationForm", propertyLocation);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SaveLocation(PropertyLocation propertyLocation)
        {
            if (!ModelState.IsValid)
            {
                return View("LocationForm", propertyLocation);
            }

            if (propertyLocation.Id == 0)
            {
                _context.PropertyLocations.Add(propertyLocation);
            }
            else
            {
                var propertyLocationInDB = _context.PropertyLocations.SingleOrDefault(m => m.Id == propertyLocation.Id);
                propertyLocationInDB.ShortCode = propertyLocation.ShortCode;
                propertyLocationInDB.BuildingName = propertyLocation.BuildingName;
                propertyLocationInDB.Street = propertyLocation.Street;
                propertyLocationInDB.AddressLine2 = propertyLocation.AddressLine2;
                propertyLocationInDB.City = propertyLocation.City;
                propertyLocationInDB.County = propertyLocation.County;
                propertyLocationInDB.Country = propertyLocation.Country;

            }

            _context.SaveChanges();
            return RedirectToAction("Locations", "Property");
        }

        public ActionResult NewProperty()
        {
            var viewModel = new PropertyFormViewModel()
            {   
                Property = new Property (),
                Locations = _context.PropertyLocations.ToList(),
                PropertyTypes = _context.PropertyTypes.ToList()
            };

            return View("PropertyForm", viewModel);
        }

        public ActionResult EditProperty(int id)
        {
            var viewModel = new PropertyFormViewModel()
            {
                Property = _context.Properties.SingleOrDefault(m => m.Id == id),
                Locations = _context.PropertyLocations.ToList(),
                PropertyTypes = _context.PropertyTypes.ToList()
            };

            return View("PropertyForm", viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SaveProperty(Property property)
        {
            var updatedePropertyLocation = _context.PropertyLocations.SingleOrDefault(m => m.Id == property.PropertyLocationId);
            property.PropertyName = PropertyName.Get(updatedePropertyLocation.ShortCode, property.PropertyNo);

            if (!ModelState.IsValid)
            {
                var viewModel = new PropertyFormViewModel()
                {
                    Property = property,
                    Locations = _context.PropertyLocations.ToList(),
                    PropertyTypes = _context.PropertyTypes.ToList()
                };
                return View("PropertyForm", viewModel);
            }

            if (property.Id == 0)
            {
                _context.Properties.Add(property);
            }
            else
            {
                var propertyInDb = _context.Properties.SingleOrDefault(m => m.Id == property.Id);
                propertyInDb.PropertyNo = property.PropertyNo;
                propertyInDb.PropertyName = property.PropertyName;
                propertyInDb.PropertyLocationId = property.PropertyLocationId;
                propertyInDb.PropertyTypeId = property.PropertyTypeId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Property");
        }
    }
}