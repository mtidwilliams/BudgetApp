using AutoMapper;
using BudgetApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp.Core.Common.DTOs
{
    /// <summary>
    /// Represents a Budget
    /// </summary>
    public class BudgetDTO
    {
        public Guid BudgetId { get; set; }
        public List<ExpenseDTO>? Expenses { get; set; }
        public List<IncomeSourceDTO>? IncomeSources { get; set; }
    }
}
