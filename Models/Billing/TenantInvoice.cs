using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Tenancies;

namespace BabzRent.Models.Billing
{
    public class TenantInvoice
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public IEnumerable<Bill> Bills { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public int SettledAmount { get; set; }
        public DateTime? SetllementDate { get; set; }
        public Tenancy Tenancy { get; set; }
        public int TenancyId { get; set; }
    }
}