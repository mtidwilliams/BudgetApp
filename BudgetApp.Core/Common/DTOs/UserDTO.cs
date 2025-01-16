namespace BudgetApp.Core.Common.DTOs;

public class UserDTO
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public PersonDTO Person { get; set; }
    public BudgetDTO? Budget { get; set; }
}