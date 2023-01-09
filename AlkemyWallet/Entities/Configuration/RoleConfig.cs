using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlkemyWallet.Entities.Configuration;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(r => r.Name)
            .HasColumnName("name");

        builder.Property(r => r.Description)
            .HasColumnName("description");
    }
}
