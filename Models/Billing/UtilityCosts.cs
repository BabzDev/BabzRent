using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BabzRent.Data;
using BabzRent.Models.Properties;

namespace BabzRent.Models.Billing
{
    public class UtilityCosts
    {
        public int Id { get; set; }
        public BillType BillType { get; set; }
        public byte BillTypeId { get; set; }
        public Property Property { get; set; }
        public int PropertyId { get; set; }
        public float VariableCost1 { get; set; }
        public float VariableCost2 { get; set; }
        public float FixedCost1 { get; set; }
        public float FixedCost2 { get; set; }
        public DateTime DateEffectiveFrom { get; set; }
        public DateTime? DateEffectiveExpiry { get; set; }
        public DateTime DateUpdated { get; set; }

        [NotMapped]
        private ApplicationDbContext _context { get; set; }

        public UtilityCosts()
        {

        }

        public UtilityCosts(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public UtilityCosts GetCurrentCosts(DateTime billDate, int propertyId, int billTypeId)
        {
            UtilityCosts currentCosts = _context.UtilityCosts.SingleOrDefault(c =>
                (c.DateEffectiveFrom <= billDate)
                & (c.DateEffectiveExpiry >= billDate || c.DateEffectiveExpiry == null)
                & (c.PropertyId == propertyId)
                & (c.BillTypeId == billTypeId));

            return currentCosts;
        }


    }
}