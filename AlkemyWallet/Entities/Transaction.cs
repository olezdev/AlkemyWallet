using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities;

public class Transaction : EntityBase
{
    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("concept")]
    public string Concept { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("type")]
    public string Type { get; set; }

    [Column("account_id")]
    public int AccountId { get; set; }

    [ForeignKey("AccountId")]
    public virtual Account Account { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    [Column("to_account_id")]
    public int ToAccountId { get; set; }

    //[ForeignKey("ToAccountId")]
    public virtual Account ToAccount { get; set; }

}
