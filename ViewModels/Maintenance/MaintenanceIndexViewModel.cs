using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models;
using BabzRent.Models.Billing;
using BabzRent.Models.Properties;

namespace BabzRent.ViewModels.Maintenance
{
    public class MaintenanceIndexViewModel
    {
        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<PropertyLocation> Locations { get; set; }
        public IEnumerable<BillType> BillTypes { get; set; }
    }
}