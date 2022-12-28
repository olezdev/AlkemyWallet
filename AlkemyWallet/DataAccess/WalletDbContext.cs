using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace AlkemyWallet.DataAccess;

public class WalletDbContext : DbContext
{
    public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
    {

    }

    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SeedRoles();
        modelBuilder.SeedUsers();
        modelBuilder.SeedAccounts();
        modelBuilder.SeedTransactions();

        //modelBuilder.Entity<Transaction>()
        //        .HasOne(p => p.Account)
        //        .WithMany(t => t.Acc)
        //        .HasForeignKey(m => m.)
        //        .WillCascadeOnDelete(false);
    //    modelBuilder
    //.Entity<Transaction>()
    //.HasOne(t => t.User)
    //.WithOne(t => t.Id)
    }

}
