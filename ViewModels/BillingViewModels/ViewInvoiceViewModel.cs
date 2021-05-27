using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Billing;
using BabzRent.Models;
using BabzRent.Models.Tenancies;

namespace BabzRent.ViewModels.BillingViewModels
{
    public class ViewInvoiceViewModel
    {
        public Invoice Invoice { get; set; }
        public IEnumerable<Bill> Bills { get; set; }
        public TenancyNames Tenancy { get; set; }
        public bool EditStatus { get; set; }
    }
}