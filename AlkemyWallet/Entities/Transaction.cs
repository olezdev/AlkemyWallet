using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities;

public class Transaction : EntityBase
{
    public decimal Amount { get; set; }
    public string Concept { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int? AccountId { get; set; }
    public virtual Account Account { get; set; }
    public int ToAccountId { get; set; }
    public virtual Account ToAccount { get; set; }
}
