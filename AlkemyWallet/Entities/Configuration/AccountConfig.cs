using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlkemyWallet.Entities.Configuration;

public class AccountConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(a => a.CreationDate)
            .HasColumnName("creationDate")
            .HasColumnType("date");

        builder.Property(a => a.Money)
            .HasColumnName("money")
            .HasColumnType("decimal(9, 2)");

        builder.Property(a => a.IsBlocked)
            .HasColumnName("isBlocked");

        builder.Property(a => a.UserId)
            .HasColumnName("user_id");
    }
}
