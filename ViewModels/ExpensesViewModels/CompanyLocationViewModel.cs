using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using BabzRent.Models.Expenses;

namespace BabzRent.ViewModels.ExpensesViewModels
{
    public class CompanyLocationViewModel
    {
        public ExpenseCompanyLocation CompanyLocation { get; set; }
        public IEnumerable<ExpenseCompany> Companies { get; set; }
    }
}
