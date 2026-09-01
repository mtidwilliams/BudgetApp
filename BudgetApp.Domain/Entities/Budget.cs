namespace BudgetApp.Domain.Entities
{
    public class Budget
    {
        public Guid BudgetId { get; set; }
        public virtual List<Expense>? Expenses { get; set; } = new List<Expense>();
        public virtual List<IncomeSource>? IncomeSources { get; set; } = new List<IncomeSource>();
    }
}
