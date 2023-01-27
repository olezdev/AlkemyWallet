using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Models.DTO;

public class UserDetailsDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Points { get; set; }
    public string Role { get; set; }
}
