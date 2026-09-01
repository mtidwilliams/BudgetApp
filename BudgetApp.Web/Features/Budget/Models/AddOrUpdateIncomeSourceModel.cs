public class AddOrUpdateIncomeSourceModel
{
    public Guid? IncomeSourceId { get; set; }
    public string IncomeSourceName { get; set; } = string.Empty;
    public decimal IncomeSourceAmount { get; set; }
}