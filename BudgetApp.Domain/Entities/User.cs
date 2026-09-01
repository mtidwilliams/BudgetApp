namespace BudgetApp.Domain.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid PersonId { get; set; }
    public virtual Person Person { get; set; }
    public Guid BudgetId { get; set; }
    public virtual Budget? Budget { get; set; }
}