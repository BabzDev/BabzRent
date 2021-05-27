using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BabzRent.Models.Properties;
using BabzRent.Models.Tenants;
using BabzRent.Data;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BabzRent.Models.Tenancies
{
    public class Tenancy
    {
        public int Id { get; set; }

        public Property Property { get; set; }
        public int PropertyId { get; set; }

        public Tenant Tenant { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant2 { get; set; }
        public int? Tenant2Id { get; set; }
        public Tenant Tenant3 { get; set; }
        public int? Tenant3Id { get; set; }
        public Tenant Tenant4 { get; set; }
        public int? Tenant4Id { get; set; }

        public DateTime MoveInDate { get; set; }
        public DateTime? MoveOutDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Deposit { get; set; }
        public int AccountBalance { get; set; }

        [NotMapped]
        public ApplicationDbContext _context { get; set; }

        public Tenancy()
        {

        }

        public Tenancy(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Tenant> GetTenants(int tenancyId)
        {
            List<Tenant> tenants = new List<Tenant>();

            var tenancy = _context.Tenancies
                .Include(i => i.Tenant)
                .Include(i => i.Tenant2)
                .Include(i => i.Tenant3)
                .Include(i => i.Tenant4)
                .Single(i => i.Id == tenancyId);

            if (tenancy.Tenant != null)
            {
                tenants.Add(tenancy.Tenant);
            }
            if (tenancy.Tenant2 != null)
            {
                tenants.Add(tenancy.Tenant2);
            }
            if (tenancy.Tenant3 != null)
            {
                tenants.Add(tenancy.Tenant3);
            }
            if (tenancy.Tenant4 != null)
            {
                tenants.Add(tenancy.Tenant4);
            }

            return tenants;
        }
    }

}
