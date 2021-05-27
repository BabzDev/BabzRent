using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models;
using BabzRent.Models.Tenancies;
using BabzRent.Data;
using BabzRent.ViewModels;



namespace BabzRent.Models.Tenancies
{
    public class TenancyRepository
    {
        private ApplicationDbContext _context { get; set; }
        public TenancyRepository()
        {

        }

        public TenancyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public static IEnumerable<Tenancy> ActiveTenancies(IEnumerable<Tenancy> tenancies)
        {
            var tenanciesOutput = tenancies.Where(m => m.MoveOutDate >= DateTime.Now || m.MoveOutDate == null).ToList();
            return tenanciesOutput;
        }

        public static IEnumerable<Tenancy> ActiveTenancies(IEnumerable<Tenancy> tenancies, DateTime contractExpiryDate)
        {
            if (contractExpiryDate >= DateTime.Now)
            {
                var tenanciesOutput = tenancies.Where(m => m.MoveOutDate >= DateTime.Now || m.MoveOutDate == null).ToList();
                return tenanciesOutput;
            }
            else
                return tenancies;
        }

        public static List<Tenancy> ActiveTenanciesPlus3MonthsBack(List<Tenancy> tenancies)
        {
            tenancies = tenancies.Where(t => t.MoveOutDate == null || t.MoveOutDate >= DateTime.Now.AddMonths(-3)).ToList();
            return tenancies;
        }

        public Tenancy GetActiveTenancyAtDateForProperty(int propertyId, DateTime date)
        {
            //Checks if any live tenancies during the MeterReading Date
            Tenancy tenancy = _context.Tenancies.SingleOrDefault(
                m => m.PropertyId == propertyId
                & (m.MoveOutDate == null || m.MoveOutDate >= date)
                & (m.MoveInDate < date));

            return tenancy;
        }

        public void SaveTenancy(TenancyFormViewModel viewModel)
        {
            var tenancy = viewModel.Tenancy;
            tenancy.LastUpdated = DateTime.Now;
            tenancy.AccountBalance = AccountBalance.Get(tenancy.Id);

            if (tenancy.Id == 0)
            {
                _context.Tenancies.Add(tenancy);
                _context.SaveChanges();

                viewModel.Contract.TenancyId = tenancy.Id;

                var contractRepositroy = new ContractRepository(_context);
                contractRepositroy.SaveContract(viewModel.Contract);
            }
            else
            {
                var tenancyInDb = _context.Tenancies.Single(m => m.Id == tenancy.Id);
                tenancyInDb.PropertyId = tenancy.PropertyId;
                tenancyInDb.TenantId = tenancy.TenantId;
                tenancyInDb.Tenant2Id = tenancy.Tenant2Id;
                tenancyInDb.Tenant3Id = tenancy.Tenant3Id;
                tenancyInDb.Tenant4Id = tenancy.Tenant4Id;
                tenancyInDb.MoveInDate = tenancy.MoveInDate;
                tenancyInDb.MoveOutDate = tenancy.MoveOutDate;
                tenancyInDb.LastUpdated = tenancy.LastUpdated;
                tenancyInDb.Deposit = tenancy.Deposit;
                _context.SaveChanges();
            }
        }
    }

}