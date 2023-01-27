using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlkemyWallet.Entities.Configuration;

public class TransactionConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => new { t.Id });

        builder.Property(t => t.Amount)
            .HasColumnName("amount")
            .HasColumnType("decimal(9,2)");

        builder.Property(t => t.Concept)
            .HasColumnName("concept");

        builder.Property(t => t.Date)
            .HasColumnName("date");

        builder.Property(t => t.Type)
            .HasColumnName("type");

        builder.Property(t => t.UserId)
            .HasColumnName("user_id");

        builder
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(t => t.AccountId)
            .HasColumnName("account_id");

        builder
            .HasOne(t => t.Account)
            .WithMany()
            .HasForeignKey(t => t.AccountId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(t => t.ToAccountId)
            .HasColumnName("to_account_id");

        builder
            .HasOne(t => t.ToAccount)
            .WithMany()
            .HasForeignKey(t => t.ToAccountId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
