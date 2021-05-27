using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Maintenance;
using BabzRent.Models;
using BabzRent.Models.Billing;
using BabzRent.Models.Properties;

namespace BabzRent.ViewModels.MeterViewModels
{
    public class MeterFormViewModel
    {
        public Meter Meter { get; set; }
        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<BillType> BillTypes { get; set; }
        public UtilityCosts UtilityCosts { get; set; }
    }
}