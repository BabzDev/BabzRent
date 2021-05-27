using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabzRent.Models.Expenses;
using BabzRent.Models.Properties;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BabzRent.ViewModels.ExpensesViewModels
{
    public class ExpenseViewModel
    {
        public IEnumerable<Expense> Expenses { get; set; }
        public IEnumerable<ExpenseCompany> Companies { get; set; }
        public IEnumerable<ExpenseCompanyLocation> CompanyLocations { get; set; }
        public IEnumerable<PropertyLocation> BillableLocations { get; set; }
        public IEnumerable<Property> BillableProperties { get; set; }
    }
}
