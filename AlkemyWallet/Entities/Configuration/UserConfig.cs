using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlkemyWallet.Entities.Configuration;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName)
            .HasColumnName("first_name");

        builder.Property(u => u.LastName)
            .HasColumnName("last_name");

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .IsRequired();

        builder.Property(u => u.Password)
            .HasColumnName("password")
            .IsRequired();

        builder.Property(u => u.Points)
            .HasColumnName("points");

        builder.Property(t => t.RoleId)
            .HasColumnName("rol_id");
    }
}
