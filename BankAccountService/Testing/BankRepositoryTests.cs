using Microsoft.EntityFrameworkCore;
using Xunit;
using BankAccountService.Models;
//using BankAccountService.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;


public class BankRepositoryTests
{
    private readonly BankRepository _repository;
    private readonly BankContext _context;

    public BankRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<BankContext>()
            .UseInMemoryDatabase(databaseName: "BankTestDatabase")
            .Options;

        _context = new BankContext(options);
        _repository = new BankRepository(_context);

        // Seed data for tests
        SeedData();
    }

    private void SeedData()
    {
        var accountHolder = new AccountHolder
        {
            Id = 1,
            FirstName = "Test",
            LastName = "User",
            DateOfBirth = new DateTime(1985, 1, 1),
            IDNumber = "123456789",
            ResidentialAddress = "123 Main St",
            MobileNumber = "123-456-7890",
            Email = "test.user@example.com",
            BankAccounts = new List<BankAccount>
            {
                new BankAccount { Id = 1, AccountNumber = "001", AccountType = "Savings", Name = "Test Account", Status = "Active", AvailableBalance = 1000 }
            }
        };

        _context.AccountHolders.Add(accountHolder);
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetBankAccountsAsync_ShouldReturnAccounts()
    {
        // Arrange
        var accountHolderId = 1;

        // Act
        var accounts = await _repository.GetBankAccountsAsync(accountHolderId);

        // Assert
        Assert.Single(accounts);
    }
}
