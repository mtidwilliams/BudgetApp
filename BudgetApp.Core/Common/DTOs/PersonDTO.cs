namespace BudgetApp.Core.Common.DTOs;

public class PersonDTO
{
    public Guid PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AddressDTO Address { get; set; }
    public UserDTO User { get; set; }
}