using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models;
using BabzRent.Models.Tenancies;

namespace BabzRent.ViewModels
{
    public class ContractFormViewModel
    {
        public Contract Contract { get; set; }
        public IEnumerable<TenancyNames> Tenancies { get; set; }

    }
}