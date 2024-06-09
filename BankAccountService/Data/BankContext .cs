using BankAccountService.Models;
using Microsoft.EntityFrameworkCore;

public class BankContext : DbContext
{
    public DbSet<AccountHolder> AccountHolders { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Withdrawal> Withdrawals { get; set; }

    public BankContext(DbContextOptions<BankContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankAccount>()
           .Property(b => b.AvailableBalance)
           .HasColumnType("decimal(18,2)");
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Withdrawal>()
            .Property(w => w.Amount)
            .HasColumnType("decimal(18,2)");

        // Seed data
        modelBuilder.Entity<AccountHolder>().HasData(
            new AccountHolder
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1985, 1, 1),
                IDNumber = "123456789",
                ResidentialAddress = "123 Main St",
                MobileNumber = "123-456-7890",
                Email = "john.doe@example.com"
            }
        );

        modelBuilder.Entity<BankAccount>().HasData(
            new BankAccount
            {
                Id = 1,
                AccountNumber = "001",
                AccountType = "Savings",
                Name = "John's Savings",
                Status = "Active",
                AvailableBalance = 1000,
                AccountHolderId = 1
            },
            new BankAccount
            {
                Id = 2,
                AccountNumber = "002",
                AccountType = "Fixed Deposit",
                Name = "John's FD",
                Status = "Active",
                AvailableBalance = 5000,
                AccountHolderId = 1
            }
        );
    }
}
