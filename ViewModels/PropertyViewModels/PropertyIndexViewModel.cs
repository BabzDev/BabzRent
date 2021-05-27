using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models;
using BabzRent.Models.Properties;

namespace BabzRent.ViewModels.PropertyViewModels
{
    public class PropertyIndexViewModel
    {
        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<PropertyLocation> PropertyLocations { get; set; }
    }
}