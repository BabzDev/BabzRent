using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BabzRent.Data;
using BabzRent.Models.Properties;

namespace BabzRent.Models.Validations
{
    public class PropertyExists : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = (Property)validationContext.ObjectInstance;
             ApplicationDbContext _context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            if (property.Id == 0)
            {
                var propertyShortCode = _context.PropertyLocations.SingleOrDefault(m => m.Id == property.PropertyLocationId).ShortCode;
                var propertyName = PropertyName.Get(propertyShortCode, property.PropertyNo);

                var numberOfProperties = _context.Properties.Where(m => m.PropertyName == propertyName).ToList().Count();

                if (numberOfProperties != 0)
                {
                    return new ValidationResult("A property with this name already exists.");
                };
            };


            return ValidationResult.Success;
        }

    }
}