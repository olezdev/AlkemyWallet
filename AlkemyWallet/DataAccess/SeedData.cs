using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace AlkemyWallet.DataAccess;

public class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
         new Role()
         {
             Id = 1,
             Name = "Admin",
             Description = "Usuario Administrador"
         },
         new Role()
         {
             Id = 2,
             Name = "Regular",
             Description = "Usuario Regular"
         }
        );

        modelBuilder.Entity<User>().HasData(
         new User()
         {
             Id = 1,
             FirstName = "UsuarioAdmin",
             LastName = "Alkemy",
             Points = 1,
             Email = "admin@gmail.com",
             Password = "Password@123",
             RoleId = 1
         },
         new User()
         {
             Id = 2,
             FirstName = "UsuarioRegular1",
             LastName = "test",
             Points = 1,
             Email = "regular1@gmail.com",
             Password = "Password@123",
             RoleId = 2
         },
         new User()
         {
             Id = 3,
             FirstName = "UsuarioRegular2",
             LastName = "test",
             Points = 1,
             Email = "regular2@gmail.com",
             Password = "Password@123",
             RoleId = 2
         }
        );

        modelBuilder.Entity<Account>().HasData(
         new Account()
         {
             Id = 1,
             UserId = 2,
             CreationDate = DateTime.Now,
             IsBlocked = false,
             Money = 10000.00m
         },
         new Account()
         {
             Id = 2,
             UserId = 3,
             CreationDate = DateTime.Now,
             IsBlocked = false,
             Money = 20000.00m
         });


        modelBuilder.Entity<Transaction>().HasData(
         new Transaction()
         {
             Id = 1,
             Amount = 1000,
             Concept = "Test",
             Date = DateTime.Parse("10/12/2022"),
             Type = "Deposito",
             AccountId = 1,
             UserId = 2,
             ToAccountId = 1
         },
         new Transaction()
         {
             Id = 2,
             Amount = 500,
             Concept = "Test",
             Date = DateTime.Parse("15/12/2022"),
             Type = "Transferencia",
             AccountId = 1,
             UserId = 2,
             ToAccountId = 2
         }
        );

    }
}