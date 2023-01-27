
namespace AlkemyWallet.Core.Models.DTO; 

public class AccountsDTO 
{
    public int Id { get; set; }
    public double Money { get; set; }
    public int UserId { get; set; }
    public bool IsBlocked { get; set; }
}
