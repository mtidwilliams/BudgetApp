namespace BudgetApp.Domain.Entities
{
    public class IncomeSource
    {
        public Guid IncomeSourceId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}