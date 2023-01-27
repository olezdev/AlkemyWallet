
namespace AlkemyWallet.Entities;

public class Account : EntityBase
{
    public DateTime CreationDate { get; set; }
    public decimal Money { get; set; }
    public bool IsBlocked { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
