namespace BudgetApp.Domain.Entities
{
    public class Expense
    {
        public Guid ExpenseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int SortOrder { get; set; }
    }
}