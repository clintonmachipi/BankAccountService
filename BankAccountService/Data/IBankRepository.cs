using BankAccountService.Models;

public interface IBankRepository
{
    Task<IEnumerable<BankAccount>> GetBankAccountsAsync(int accountHolderId);
    Task<bool> CreateWithdrawalAsync(Withdrawal withdrawal);
    Task<BankAccount> GetBankAccountByNumberAsync(string accountNumber);

    Task SaveAsync();
}
