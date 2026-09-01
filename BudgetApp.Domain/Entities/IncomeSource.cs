namespace BudgetApp.Domain.Entities
{
    public class IncomeSource
    {
        public Guid IncomeSourceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int SortOrder { get; set; }
    }
}