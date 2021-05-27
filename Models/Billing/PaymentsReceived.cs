using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Tenancies;

namespace BabzRent.Models.Billing
{
    public class PaymentsReceived
    {
        public int Id { get; set; }
        public Tenancy Tenancy { get; set; }
        public int TenancyId { get; set; }
        public int PaymentAmount { get; set; }
        public DateTime DatePaymentReceived { get; set; }
        public TenantInvoice TenantInvoice { get; set; }
        public int? TenantInvoiceId { get; set; }
    }
}