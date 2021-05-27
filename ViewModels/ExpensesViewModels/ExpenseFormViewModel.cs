using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabzRent.Models.Expenses;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BabzRent.Models.Properties;

namespace BabzRent.ViewModels.ExpensesViewModels
{
    public class ExpenseFormViewModel
    {
        public Expense Expense { get; set; }
        public IEnumerable<ExpenseCompany> Companies { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
        public IEnumerable<PropertyLocation> BillableLocations { get; set; }
        public IEnumerable<SelectListItem> BillableProperties { get; set; }
    }
}