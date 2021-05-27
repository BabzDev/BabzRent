using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Billing;
using BabzRent.Models.Tenancies;
using BabzRent.Models;
using BabzRent.Models.Properties;

namespace BabzRent.ViewModels.BillingViewModels
{
    public class BillsIndexViewModel
    {
        public IEnumerable<Bill> Bills { get; set; }
        public IEnumerable<TenancyNames> TenancyNames { get; set; }
        public IEnumerable<PropertyLocation> PropertyLocations { get; set; }
        public IEnumerable<Property> Properties { get; set; }
    }
}