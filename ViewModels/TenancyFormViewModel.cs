using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Properties;
using BabzRent.Models.Tenants;
using BabzRent.Models.Tenancies;
using BabzRent.Models;

namespace BabzRent.ViewModels
{
    public class TenancyFormViewModel
    {
        public Tenancy Tenancy { get; set; }
        public IEnumerable<TenantsFullName> Tenants { get; set; }
        public IEnumerable<Property> Properties { get; set; }
        public Contract Contract { get; set; }
    }

}