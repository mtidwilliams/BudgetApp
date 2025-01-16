using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp.Domain.Entities
{
    public class Budget
    {
        public Guid BudgetId { get; set; }
        public List<Expense>? Expenses { get; set; } = new List<Expense>();
        public List<IncomeSource>? IncomeSources { get; set; } = new List<IncomeSource>();
    }
}
