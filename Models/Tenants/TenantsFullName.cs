using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabzRent.Models.Tenants
{
    public class TenantsFullName
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public static IEnumerable<TenantsFullName> GetList(IEnumerable<Tenant> tenants)
        {
            var tenantsNamesOnlyList = new List<TenantsFullName>();
            foreach (var tenant in tenants)
            {
                tenantsNamesOnlyList.Add(new TenantsFullName()
                {
                    Name = tenant.FirstName + " " + tenant.LastName,
                    Id = tenant.Id
                }); ;
            }
            return tenantsNamesOnlyList;
        }
    }
}