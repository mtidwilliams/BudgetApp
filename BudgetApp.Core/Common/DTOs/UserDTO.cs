namespace BudgetApp.Core.Common.DTOs;

public class UserDTO
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public required PersonDTO Person { get; set; }
    public BudgetDTO? Budget { get; set; }
}