using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Models.DTO;

public class TransactionDetailsDTO
{
    public string Type { get; set; }
    public string Concept { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string AccountSource { get; set; }
    public string AccountDestination { get; set; }
}
