using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Maintenance;
using BabzRent.Models;
using BabzRent.Models.Properties;

namespace BabzRent.ViewModels.MeterViewModels
{
    public class MeterReadingFormViewModel
    {
        public MeterReading MeterReading { get; set; }
        public IEnumerable<MeterNames> Meters { get; set; }
        public IEnumerable<Property> Properties { get; set; }
    }
}