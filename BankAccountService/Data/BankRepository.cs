using BankAccountService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class BankRepository : IBankRepository
{
    private readonly BankContext _context;

    public BankRepository(BankContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BankAccount>> GetBankAccountsAsync(int accountHolderId)
    {
        return await _context.BankAccounts.Where(ba => ba.AccountHolderId == accountHolderId).ToListAsync();
    }

    public async Task<BankAccount> GetBankAccountByNumberAsync(string accountNumber)
    {
        return await _context.BankAccounts.FirstOrDefaultAsync(ba => ba.AccountNumber == accountNumber);
    }

    public async Task<bool> CreateWithdrawalAsync(Withdrawal withdrawal)
    {
        var account = await _context.BankAccounts.FindAsync(withdrawal.BankAccountId);
        if (account == null) return false;

        account.AvailableBalance -= withdrawal.Amount;
        _context.Withdrawals.Add(withdrawal);
        _context.BankAccounts.Update(account);
        await SaveAsync();

        return true;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}