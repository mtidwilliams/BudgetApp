namespace BudgetApp.Core.Common.DTOs;

public class PersonDTO
{
    public Guid PersonId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public required AddressDTO Address { get; set; }
    public required UserDTO User { get; set; }
}