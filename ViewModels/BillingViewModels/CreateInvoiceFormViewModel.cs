using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Tenancies;

namespace BabzRent.ViewModels.BillingViewModels
{
    public class CreateInvoiceFormViewModel
    {
        public IEnumerable<TenancyNames> Tenancies { get; set; }
        public int tenancyId { get; set; }

    }
}