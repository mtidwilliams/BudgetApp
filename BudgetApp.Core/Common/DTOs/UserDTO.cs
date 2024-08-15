namespace BudgetApp.Core.Common.DTOs;

public class UserDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public PersonDTO Person { get; set; }
}