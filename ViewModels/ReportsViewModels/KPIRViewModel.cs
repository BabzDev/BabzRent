using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Billing;
using BabzRent.Models;
using BabzRent.Models.Tenancies;
using BabzRent.Models.Reports;

namespace BabzRent.ViewModels.ReportsViewModels
{
    public class KPIRViewModel
    {
        public IEnumerable<KPIR> KPIRs { get; set; }
        public IEnumerable<TenancyNames> Tenancies { get; set; }
    }
}