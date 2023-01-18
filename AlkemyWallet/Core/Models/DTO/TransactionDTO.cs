using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Core.Models.DTO;

public class TransactionDTO
{
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public string Concept { get; set; }
    [Required]
    public int ToAccountId { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public int AccountId { get; set; }
}
