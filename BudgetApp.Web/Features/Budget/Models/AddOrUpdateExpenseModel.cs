public class AddOrUpdateExpenseModel
{
    public Guid? ExpenseId { get; set; }
    public string ExpenseName { get; set; } = string.Empty;
    public decimal ExpenseAmount { get; set; }
}