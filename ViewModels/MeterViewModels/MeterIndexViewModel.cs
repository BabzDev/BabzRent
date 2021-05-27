using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models;
using BabzRent.Models.Maintenance;
using BabzRent.Models.Properties;

namespace BabzRent.ViewModels.MeterViewModels
{
    public class MeterIndexViewModel
    {
        public IEnumerable<Meter> Meter { get; set; }
        public IEnumerable<MeterReading> MeterReadings { get; set; }
        public IEnumerable<PropertyLocation> Locations { get; set; }
    }
}