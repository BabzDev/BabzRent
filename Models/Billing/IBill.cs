using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabzRent.Models.Maintenance;

namespace BabzRent.Models.Billing
{
    public interface IBill
    {
        Bill GenerateBill(int propertyId, byte billTypeId, MeterReading meterReading);
        Bill GenerateBill(int tenancyId);

    }        
}