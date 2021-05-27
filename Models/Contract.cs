using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BabzRent.Data;
using BabzRent.Models;
using BabzRent.Models.Tenancies;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace BabzRent.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public Tenancy Tenancy { get; set; }
        public int TenancyId { get; set; }
        public float PaymentAmount { get; set; }

        public DateTime ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public int? ContractDuration { get; set; }
        public int? ContractVersion { get; set; }  
        public int BillingDate { get; set; }

        public float? WaterIncluded { get; set; }
        public float? HotWaterIncluded { get; set; }
        public float? GasIncluded { get; set; }
        public float? ElectricityIncluded { get; set; }
        public float? RubbishIncluded { get; set; }

        //When Adding any additional UtilityBills etc add them to Bill.MonthlyBillInclusiveUnitAmount(...)

    }

    public class ContractRepository
    {
        private ApplicationDbContext _context { get; set; }

        public ContractRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public void SaveContract(Contract contract)
        {
            contract.DateCreated = DateTime.Now;

            var currentContracts = _context.Contracts.Where(m => m.TenancyId == contract.TenancyId).ToList();
            contract.ContractVersion = currentContracts.Count() + 1;

            //Update ContractEndDate if Duration is populate, else populate with 0

            if (contract.ContractDuration != 0)
                contract.ContractEndDate = contract.ContractStartDate.AddMonths((int)contract.ContractDuration);
            else
                contract.ContractEndDate = null;

            contract.BillingDate = contract.BillingDate == 0 ? 1 : contract.BillingDate;

            if (contract.Id == 0)
            {
                _context.Contracts.Add(contract);
            }
            else
            {
                var contractInDb = _context.Contracts.Single(m => m.Id == contract.Id);
                contractInDb.TenancyId = contract.TenancyId;
                contractInDb.ContractStartDate = contract.ContractStartDate;
                contractInDb.ContractDuration = contract.ContractDuration;
                contractInDb.ContractEndDate = contract.ContractEndDate;
                contractInDb.PaymentAmount = contract.PaymentAmount;
                contractInDb.DateCreated = contract.DateCreated;
                contractInDb.PaymentAmount = contract.PaymentAmount;
                contractInDb.BillingDate = contract.BillingDate;
            }

            _context.SaveChanges();
        }
    }
}