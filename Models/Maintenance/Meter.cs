using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Billing;
using BabzRent.Models.Properties;

namespace BabzRent.Models.Maintenance
{
    public class Meter
    {
        public int Id { get; set; }
        public BillType BillType { get; set; }
        public byte BillTypeId { get; set; }
        public Property Property { get; set; }
        public int PropertyId { get; set; }
    }

    public class MeterNames
    {
        //Only used for display purposes
        public int Id { get; set; }
        public string Name { get; set; }

        public static IEnumerable<MeterNames> GetList(IEnumerable<Meter> meters)
        {
            var meterNames = new List<MeterNames>();

            foreach (var meter in meters)
            {
                meterNames.Add(new MeterNames()
                {
                    Id = meter.Id,
                    Name = meter.Property.PropertyName + "-" + meter.BillType.Name
                });
            }
            return meterNames;
        }
    }
}