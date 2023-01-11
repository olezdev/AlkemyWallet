using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Core.Models.DTO;

public class TransactionCreatedDTO
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Concept { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int UserId { get; set; }
    public int? AccountId { get; set; }
    public int ToAccountId { get; set; }
}
