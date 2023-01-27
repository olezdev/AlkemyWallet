using AlkemyWallet.Entities;
using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Core.Models.DTO;

public class TransactionToCreateDTO
{
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public string Concept { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int? AccountId { get; set; }
    [Required]
    public int ToAccountId { get; set; }
}
