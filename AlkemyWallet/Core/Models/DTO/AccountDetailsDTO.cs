
namespace AlkemyWallet.Core.Models.DTO;

public class AccountDetailsDTO
{
    public int UserId { get; set; }
    public string User { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreationDate { get; set; }
    public double Money { get; set; }
    public bool IsBlocked { get; set; }
}
