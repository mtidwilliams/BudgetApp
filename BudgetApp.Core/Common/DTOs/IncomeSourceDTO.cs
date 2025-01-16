using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp.Core.Common.DTOs
{
    public class IncomeSourceDTO
    {
        public Guid IncomeSourceId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
