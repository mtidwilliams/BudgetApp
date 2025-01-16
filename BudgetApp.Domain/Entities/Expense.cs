namespace BudgetApp.Domain.Entities
{
    public class Expense
    {
        public Guid ExpenseId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}