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
    public class MultiReadingFormViewModel
    {
        public List<MeterReading> MeterReadings { get; set; }
        public IEnumerable<MeterNames> MeterNames { get; set; }
        public IEnumerable<Meter> Meters { get; set; }
        public IEnumerable<Property> Properties { get; set; }
        public IEnumerable<BillType> BillTypes { get; set; }
        public IEnumerable<MeterReading> CurrentMeterReadings { get; set; }
    }
}