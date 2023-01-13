
namespace AlkemyWallet.Core.Models.DTO;

public class TransactionsDTO
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string Concept { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}
