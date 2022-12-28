using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities;

public class Account : EntityBase
{
    [Column("creationDate")]
    public DateTime CreationDate { get; set; }

    [Column("money")]
    public double Money { get; set; }

    [Column("isBlocked")]
    public bool IsBlocked { get; set; }

    [Column("user_id")]
    [ForeignKey("User")]
    public int UserId { get; set; }

    public User User { get; set; }
}
