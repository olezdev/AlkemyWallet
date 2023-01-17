namespace AlkemyWallet.Core.Models.DTO;

public class AccountUpdatedDTO
{
    public int UserId { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsBlocked { get; set; }
}
