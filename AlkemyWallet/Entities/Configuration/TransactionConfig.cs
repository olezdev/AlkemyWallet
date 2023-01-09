using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

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
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(t => t.AccountId)
            .HasColumnName("account_id");

        builder
            .HasOne(x => x.Account)
            .WithMany()
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(t => t.ToAccountId)
            .HasColumnName("to_account_id");

        builder
            .HasOne(x => x.ToAccount)
            .WithMany()
            .HasForeignKey(x => x.ToAccountId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
