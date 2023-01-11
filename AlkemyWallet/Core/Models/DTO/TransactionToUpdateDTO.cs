using System.ComponentModel.DataAnnotations;

namespace AlkemyWallet.Core.Models.DTO;

public class TransactionToUpdateDTO
{
    [Required]
    public string Concept { get; set; }
}
