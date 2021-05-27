using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
using BabzRent.Models.Validations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using BabzRent.Data;
using Microsoft.EntityFrameworkCore;

namespace BabzRent.Models.Properties
{
    public class Property
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string PropertyName { get; set; }
        [Required, StringLength(15), PropertyExists]
        public string PropertyNo { get; set; }
        public PropertyLocation PropertyLocation { get; set; }
        public int PropertyLocationId { get; set; }
        public PropertyType PropertyType { get; set; }
        public byte PropertyTypeId { get; set; }

        [NotMapped]
        private ApplicationDbContext _context { get; set; }

        public Property()
        {

        }

        public Property(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public static IEnumerable<SelectListItem> GetPropertySelectList()
        {
            List<SelectListItem> property = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };
            return property;
        }

        public IEnumerable<SelectListItem> GetPropertySelectList(int locationId)
        {
            if (locationId != 0)
            {
                IEnumerable<SelectListItem> property = _context.Properties.AsNoTracking()
                    .OrderBy(p => p.PropertyNo)
                    .Where(p => p.PropertyLocationId == locationId)
                    .Select(p =>
                        new SelectListItem
                        {
                            Value = p.Id.ToString(),
                            Text = p.PropertyNo
                        })
                    .ToList();
                return property;
            }
            return null;
        }

    }

    public class PropertyName
    {
        public static string Get(string LocationShortCode, string PropertyNo)
        {
            var propertyName = LocationShortCode + "-" + PropertyNo;
            return propertyName;
        }
    }
}
