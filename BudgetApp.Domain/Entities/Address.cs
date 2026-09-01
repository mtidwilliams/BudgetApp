namespace BudgetApp.Domain.Entities;

public class Address
{
    public Guid AddressId { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public int ZipCode { get; set; }
}