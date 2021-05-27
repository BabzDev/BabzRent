using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models;
using BabzRent.Models.Maintenance;
using BabzRent.Models.Tenancies;
using BabzRent.Models.Properties;
using BabzRent.Models.Tenants;

namespace BabzRent.ViewModels.PropertyViewModels
{
    public class PropertyDetailsViewModel
    {
        public Property Property { get; set; }
        public IEnumerable<Meter> Meters { get; set; }
        public IEnumerable<MeterReading> MeterReadings { get; set; }
        public IEnumerable<Tenancy> Tenancies { get; set; }
        public IEnumerable<Tenant> Tenants { get; set; }
        public IEnumerable<Contract> Contracts { get; set; }
    }
}