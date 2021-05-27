using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models;
using BabzRent.Models.Properties;

namespace BabzRent.ViewModels.ReportsViewModels
{
    public class KPIRYearFormViewModel
    {
        public IEnumerable<PropertyLocation> Locations { get; set; }
        public IEnumerable<int> Dates { get; set; }
        public int LocationId { get; set; }
        public int ReportingYear { get; set; }
    }
}