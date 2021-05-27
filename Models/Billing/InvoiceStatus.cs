using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabzRent.Models.Billing
{
    public class InvoiceStatus
    {
        public byte Id { get; set; }
        public string Description { get; set; }
        public string DescriptionPL { get; set; }

        public static List<int> NotInvoicedStatusIds()
        {
            //This is hard Coded from the database tables
            return new List<int>() { 0, 2 };
        }

        public static byte PostInvoicingStatusId(byte invoiceStatusId)
        {
            if (invoiceStatusId == 0)
            {
                invoiceStatusId = 1;
            }
            else if (invoiceStatusId == 2)
            {
                invoiceStatusId = 3;
            }
            return invoiceStatusId;
        }
    }
}