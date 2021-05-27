using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabzRent.Models.Expenses;

namespace BabzRent.ViewModels.ExpensesViewModels
{
    public class CompanyFormViewModel
    {
        public ExpenseCompany Company { get; set; }
        public ExpenseCompanyLocation CompanyLocation { get; set; }
    }
}
