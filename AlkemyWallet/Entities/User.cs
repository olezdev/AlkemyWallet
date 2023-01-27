using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Entities;

public class User : EntityBase
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Points { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
}
