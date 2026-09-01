namespace BudgetApp.Domain.Entities;

public class Person
{
    public Guid PersonId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public virtual Address Address { get; set; }
}