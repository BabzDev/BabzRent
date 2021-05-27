using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Properties;

namespace BabzRent.ViewModels.BillingViewModels
{
    public class LocationFormViewModel
    {
        public IEnumerable<PropertyLocation> Locations { get; set; }
        public int locationId { get; set; }
    }
}