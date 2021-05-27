using BabzRent.Models.Billing;
using BabzRent.Models.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BabzRent.ViewModels.BillingViewModels
{
    public class InvoicePreviewViewModel
    {
        public Invoice Invoice { get; set; }
        public IEnumerable<Bill> Bills { get; set; }
        public IEnumerable<BillType> BillTypes { get; set; }
        public IEnumerable<Tenant> Tenants { get; set; }
    }
}
