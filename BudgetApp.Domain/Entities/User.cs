namespace BudgetApp.Domain.Entities;

class User
{
    public string Email { get; set; }
    public string Password { get; set; }
    public Person Person { get; set; }
}