using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BabzRent.Models.Tenants;
using System.Linq;
using System.Web;

namespace BabzRent.Models.Validations
{
    public class TenantFirstNameNoBusiness : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var tenant = (Tenant)validationContext.ObjectInstance;

            if (tenant.CompanyName == null)
            {
                if (tenant.FirstName == null)
                    return new ValidationResult("First name is necessary for none business tenants");
            }
            
            return ValidationResult.Success;
        }
    }

    public class TenantLastNameNoBusiness : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var tenant = (Tenant)validationContext.ObjectInstance;

            if (tenant.CompanyName == null)
            {
                if (tenant.LastName == null)
                    return new ValidationResult("Last name is necessary for none business tenants");
            }

            return ValidationResult.Success;
        }
    }

    public class TenantBirthDateNoBusiness : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var tenant = (Tenant)validationContext.ObjectInstance;

            if (tenant.CompanyName == null)
            {
                if (tenant.BirthDate == null)
                    return new ValidationResult("Birth date is necessary for none business tenants");
            }

            return ValidationResult.Success;
        }
    }

    public class TenantLastNameMinLength : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var tenant = (Tenant)validationContext.ObjectInstance;

            if (tenant.LastName != null)
            {
                if (tenant.LastName.Length <4 )
                    return new ValidationResult("Last Name requires minimum 4 characters");
            }

            return ValidationResult.Success;
        }
    }
}