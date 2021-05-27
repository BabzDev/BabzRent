using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Properties;

namespace BabzRent.ViewModels
{
    public class PropertyFormViewModel
    {
        public Property Property { get; set; }
        public IEnumerable<PropertyLocation> Locations { get; set; }
        public IEnumerable<PropertyType> PropertyTypes { get; set; }
    }
}