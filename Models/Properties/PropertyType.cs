using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BabzRent.Models.Properties
{
    public class PropertyType
    {
        public byte Id { get; set; }
        [Required, StringLength(20)]
        public string Name { get; set; }
        public byte Bedrooms { get; set; }
        public byte Bathrooms { get; set; }
    }
}
