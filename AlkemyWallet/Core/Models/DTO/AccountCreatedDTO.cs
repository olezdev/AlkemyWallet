
namespace AlkemyWallet.Core.Models.DTO;

public class AccountCreatedDTO
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public double Money { get; set; }
    public int UserId { get; set; }
}
