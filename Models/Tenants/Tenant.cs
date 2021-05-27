using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BabzRent.Models.Validations;
using BabzRent.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabzRent.Models.Tenants
{
    public class Tenant
    {
        [Display(Name = "Tenant Id")]
        public int Id { get; set; }

        [StringLength(255), Display(Name = "First name"), TenantFirstNameNoBusiness]
        public string FirstName { get; set; }

        [StringLength(255), Display(Name = "Last name"), TenantLastNameNoBusiness, TenantLastNameMinLength]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth"), TenantBirthDateNoBusiness]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Contact Number"), Phone]
        public string ContactNumber { get; set; }

        [Display(Name = "Document number")]
        public string IdNumber { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
