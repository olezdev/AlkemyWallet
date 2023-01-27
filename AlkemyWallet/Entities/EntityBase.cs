using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlkemyWallet.Entities;

public class EntityBase
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

}
