using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabzRent.Data;
using BabzRent.Models;
using BabzRent.Models.Tenants;

namespace BabzRent.Controllers
{
    public class TenantsController : Controller
    {
        private ApplicationDbContext _context { get; set; }

        public TenantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Tenants
        public ActionResult Index()
        {
            var tenants = _context.Tenants.ToList();
            return View(tenants);
        }

        public ActionResult New()
        {
            var tenant = new Tenant();
            return View("TenantForm", tenant);
        }

        public ActionResult Details(int id)
        {
            var tenant = _context.Tenants.SingleOrDefault(m => m.Id == id);
            return View(tenant);
        }

        public ActionResult Edit(int id)
        {
            var tenant = _context.Tenants.SingleOrDefault(m => m.Id == id);
            return View("TenantForm",tenant);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Save(Tenant tenant)
        {
            if (!ModelState.IsValid)
                return View("TenantForm", tenant);

            tenant.LastUpdated = DateTime.Now;

            if (tenant.Id == 0)
            {
                tenant.DateCreated = DateTime.Now;
                _context.Tenants.Add(tenant);
            }
            else
            {
                var tenantInDb = _context.Tenants.SingleOrDefault(m => m.Id == tenant.Id);
                tenantInDb.FirstName = tenant.FirstName;
                tenantInDb.LastName = tenant.LastName;
                tenantInDb.BirthDate = tenant.BirthDate;
                tenantInDb.CompanyName = tenant.CompanyName;
                tenantInDb.ContactNumber = tenant.ContactNumber;
                tenantInDb.Email = tenant.Email;
                tenantInDb.IdNumber = tenant.IdNumber;
                tenantInDb.LastUpdated = tenant.LastUpdated;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Tenants");
        }

    }
}