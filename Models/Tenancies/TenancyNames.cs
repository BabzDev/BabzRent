using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabzRent.Models.Tenancies
{
    public class TenancyNames
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public static IEnumerable<TenancyNames> Get(IEnumerable<Tenancy> tenanciesInclProperty)
        {
            var tenanciesOutput = new List<TenancyNames>();
            foreach (var tenancy in tenanciesInclProperty)
            {
                tenanciesOutput.Add(new TenancyNames()
                {
                    Name = tenancy.Property.PropertyName + "/" + tenancy.MoveInDate.Year.ToString() + "." + tenancy.MoveInDate.Month.ToString(),
                    Id = tenancy.Id
                });
            }

            return tenanciesOutput;
        }

    }
}