using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using BabzRent.Data;
using BabzRent.ViewModels;
using BabzRent.Models;
using BabzRent.Models.Tenancies;
using BabzRent.Models.Tenants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Internal;

namespace BabzRent.Controllers
{

    public class TenancyController : Controller
    {

        public ApplicationDbContext _context { get; set; }

        public TenancyController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Rent
        public ActionResult Index()
        {
            var viewModel = new TenancyIndexViewModel()
            {
                Tenants = _context.Tenants.ToList(),
                Tenancies = _context.Tenancies.Include(m => m.Property).ToList(),
                Contracts = _context.Contracts.ToList(),
                TenancyNames = TenancyNames.Get(_context.Tenancies.Include(m => m.Property).ToList())
            };

            return View(viewModel);
        }

        public ActionResult NewTenancy(int? propertyId)
        {
            var properties = _context.Properties.ToList();

            if (propertyId != null)
                properties = properties.Where(m => m.Id == (int)propertyId).ToList();

            var viewModel = new TenancyFormViewModel()
            {
                Tenancy = new Tenancy() { MoveInDate = DateTime.Now},
                Properties = properties,
                Tenants = TenantsFullName.GetList(_context.Tenants.ToList())
            };
            return View("TenancyForm", viewModel);
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult EditTenancy(int id)
        {
            var viewModel = new TenancyFormViewModel()
            {
                Properties = _context.Properties.ToList(),
                Tenants = TenantsFullName.GetList(_context.Tenants.ToList()),
                Tenancy = _context.Tenancies.SingleOrDefault(m => m.Id == id)
            };
            return View("TenancyForm", viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SaveTenancy(TenancyFormViewModel viewModel)
        {
            var tenancyRepo = new TenancyRepository();
            tenancyRepo.SaveTenancy(viewModel);

            return RedirectToAction("Index", "Tenancy");
        }
    }
}