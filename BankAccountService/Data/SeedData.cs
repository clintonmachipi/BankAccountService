// SeedData.cs
using BankAccountService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BankAccountService.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BankContext(
                serviceProvider.GetRequiredService<DbContextOptions<BankContext>>()))
            {
                if (context.BankAccounts.Any())
                {
                    return; // DB has been seeded
                }

                var accountHolder = new AccountHolder
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    IDNumber = "1234567890",
                    ResidentialAddress = "123 Main St",
                    MobileNumber = "123-456-7890",
                    Email = "john.doe@example.com",
                };

                context.AccountHolders.Add(accountHolder);
                context.SaveChanges();

                var bankAccount = new BankAccount
                {
                    AccountNumber = "001",
                    AccountType = "Savings",
                    Name = "John's Savings Account",
                    Status = "Active",
                    AvailableBalance = 1000,
                    AccountHolderId = accountHolder.Id
                };

                context.BankAccounts.Add(bankAccount);
                context.SaveChanges();
            }
        }
    }
}
