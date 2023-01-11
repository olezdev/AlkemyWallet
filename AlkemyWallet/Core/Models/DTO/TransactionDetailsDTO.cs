
namespace AlkemyWallet.Core.Models.DTO;

public class TransactionDetailsDTO
{
    public string Type { get; set; }
    public string Concept { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int? AccountId { get; set; }
    public string User { get; set; }
    public int? ToAccountId { get; set; }
    public int ToAccountUserId { get; set; }
}
