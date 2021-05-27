using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabzRent.Models.Maintenance
{
    public class MeterReading
    {
        public int Id { get; set; }
        public Meter Meter { get; set; }
        public int MeterId { get; set; }
        public float Status { get; set; }
        public DateTime ReadingDate { get; set; }
        public DateTime DateCreated { get; set; }
    }
}