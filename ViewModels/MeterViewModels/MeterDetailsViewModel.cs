using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Maintenance;

namespace BabzRent.ViewModels.MeterViewModels
{
    public class MeterDetailsViewModel
    {
        public Meter Meter { get; set; }
        public IEnumerable<MeterReading> MeterReadings { get; set; }
    }
}