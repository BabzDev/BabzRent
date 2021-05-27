using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabzRent.Models.Billing
{
    public class BillType
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public string NamePL { get; set; }
        public string UnitType { get; set; }
        public string UnitTypePL { get; set; }

        public static List<int> GetMeteredBillTypeIds()
        {
            return new List<int> {3, 4, 5, 6 };
        }
    }
}