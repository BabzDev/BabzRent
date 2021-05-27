using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabzRent.Data;
using BabzRent.Models;
using BabzRent.ViewModels;
using BabzRent.Models.Tenancies;

namespace BabzRent.Controllers
{
    public class ContractController : Controller
    {
        private ApplicationDbContext _context { get; set; }

        public ContractController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Contracts
        public ActionResult Index()
        {
            var contracts = _context.Contracts.Include(p => p.Tenancy.Property).ToList();
            return View(contracts);
        }

        public ActionResult NewContract()
        {
            var tenanciesInclProperty = _context.Tenancies.Include(m => m.Property).ToList();

            var viewModel = new ContractFormViewModel()
            {
                Tenancies = TenancyNames.Get(TenancyRepository.ActiveTenancies(tenanciesInclProperty))
            };
            return View("ContractForm", viewModel);
        }

        public ActionResult EditContract(int id)
        {
            var tenanciesInclProperty = _context.Tenancies.Include(m => m.Property).ToList();
            var contract = _context.Contracts.SingleOrDefault(m => m.Id == id);

            var viewModel = new ContractFormViewModel()
            {
                Contract = contract,
                Tenancies = TenancyNames.Get(TenancyRepository.ActiveTenancies(tenanciesInclProperty, contract.ContractEndDate.GetValueOrDefault()))
            };
            return View("ContractForm", viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Save(Contract contract)
        {
            var contractReopositroy = new ContractRepository(_context);
            contractReopositroy.SaveContract(contract);

            return RedirectToAction("Index", "Contract");
        }
    }
}