namespace BudgetApp.Domain.Entities;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Address Address { get; set; }
    public virtual User User { get; set; }
}