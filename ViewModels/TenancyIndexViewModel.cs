using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models;
using BabzRent.Models.Tenancies;
using BabzRent.Models.Tenants;
using BabzRent.Models.Properties;

namespace BabzRent.ViewModels
{
    public class TenancyIndexViewModel
    {
        public IEnumerable<Tenancy> Tenancies { get; set; }
        public IEnumerable<Tenant> Tenants { get; set; }  
        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<Contract> Contracts { get; set; }
        public IEnumerable<TenancyNames> TenancyNames { get; set; }
    }
} 