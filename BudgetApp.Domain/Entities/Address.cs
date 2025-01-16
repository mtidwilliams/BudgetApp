namespace BudgetApp.Domain.Entities;

public class Address
{
    public Guid AddressId { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int ZipCode { get; set; }
}