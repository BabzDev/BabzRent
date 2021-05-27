using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabzRent.Models.Expenses;

namespace BabzRent.ViewModels.ExpensesViewModels
{
    public class CompaniesViewModel
    {
        public IEnumerable<ExpenseCompany> Companies { get; set; }
        public IEnumerable<ExpenseCompanyLocation> Locations { get; set; }
    }
}
