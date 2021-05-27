using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Billing;
using BabzRent.Models.Tenancies;
using BabzRent.Models.Properties;

namespace BabzRent.ViewModels.BillingViewModels
{
    public class InvoiceViewModel
    {
        public IEnumerable<Invoice> Invoices { get; set; }
        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<PropertyLocation> Locations { get; set; }
        public IEnumerable<TenancyNames> Tenancies { get; set; }

    }
}