using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Billing;
using BabzRent.Models.Tenancies;
using BabzRent.Models.Properties;

namespace BabzRent.ViewModels.BillingViewModels
{
    public class BillFormViewModel
    {
        public Bill Bill { get; set; }
        public IEnumerable<BillType> BillTypes { get; set; }
        public IEnumerable<TenancyNames> Tenancies { get; set; }
        public IEnumerable<Property> Properties { get; set; }

    }
}