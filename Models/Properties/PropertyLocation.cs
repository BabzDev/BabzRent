using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabzRent.Models.Properties
{
    public class PropertyLocation
    {
        public int Id { get; set; }
        [Required, StringLength(15)]
        public string ShortCode { get; set; }
        [StringLength(100)]
        public string BuildingName { get; set; }
        [StringLength(250)]
        public string Street { get; set; }
        [StringLength(250)]
        public string AddressLine2 { get; set; }
        [Required, StringLength(30)]
        public string City { get; set; }
        [StringLength(10)]
        public string PostCode { get; set; }
        [StringLength(30)]
        public string County { get; set; }
        [Required, StringLength(30)]
        public string Country { get; set; }
    }
}
