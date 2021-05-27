using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Properties;
using BabzRent.Data;

namespace BabzRent.Models.Billing
{
    public class UtilityCostsUpdate
    {
        public float FixedCost1 { get; set; }
        public float FixedCost2 { get; set; }
        public float VariableCost1 { get; set; }
        public float VariableCost2 { get; set; }
        public int LocationId { get; set; }
        public byte BillTypeId { get; set; }
        public DateTime DateEffectiveFrom { get; set; }
        public DateTime? DateEffectiveTo { get; set; }
        public DateTime DateUpdated { get; set; }
    }

    public class UtilityCostsCalculations 
    { 
        public ApplicationDbContext _context { get; set; }

        public UtilityCostsCalculations(ApplicationDbContext context)
        {
            _context = context;
        }

        public void UpdateAllCostsAtLocation(UtilityCostsUpdate viewModel)
        {
            
            IEnumerable<Property> properties = _context.Properties.Where(p => p.PropertyLocationId == viewModel.LocationId).ToList();

            UtilityCosts newUtilityCosts;

            foreach (var property in properties)
            {
                var oldUtilityCosts = _context.UtilityCosts.Single(b => b.PropertyId == property.Id && b.DateEffectiveExpiry == null && b.BillTypeId == viewModel.BillTypeId);
                oldUtilityCosts.DateEffectiveExpiry = viewModel.DateEffectiveFrom.AddDays(-1);

                newUtilityCosts = new UtilityCosts
                {
                    BillTypeId = viewModel.BillTypeId,
                    FixedCost1 = viewModel.FixedCost1,
                    FixedCost2 = viewModel.FixedCost2,
                    VariableCost1 = viewModel.VariableCost1,
                    VariableCost2 = viewModel.VariableCost2,
                    PropertyId = property.Id,
                    DateEffectiveFrom = viewModel.DateEffectiveFrom,
                    DateEffectiveExpiry = null,
                };

                _context.UtilityCosts.Add(newUtilityCosts);
            }

            _context.SaveChanges();
        }
    }
}