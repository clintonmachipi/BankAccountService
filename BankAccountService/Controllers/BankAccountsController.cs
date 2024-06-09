using BankAccountService.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BankAccountsController : ControllerBase
{
    private readonly IBankRepository _repository;

    public BankAccountsController(IBankRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{accountHolderId}")]
    public async Task<IActionResult> GetBankAccounts(int accountHolderId)
    {
        var accounts = await _repository.GetBankAccountsAsync(accountHolderId);
        return Ok(accounts);
    }

    [HttpGet("account/{accountNumber}")]
    public async Task<IActionResult> GetBankAccount(string accountNumber)
    {
        var account = await _repository.GetBankAccountByNumberAsync(accountNumber);
        if (account == null) return NotFound();

        return Ok(account);
    }

    [HttpPost("withdraw")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateWithdrawal([FromBody] Withdrawal withdrawal)
    {
        var account = await _repository.GetBankAccountByNumberAsync(withdrawal.BankAccount.AccountNumber);

        if (withdrawal.Amount <= 0)
        {
            return BadRequest("Withdrawal amount must be greater than 0.");
        }

        if (withdrawal.Amount > account.AvailableBalance)
        {
            return BadRequest("Insufficient funds.");
        }

        if (account.Status != "Active")
        {
            return BadRequest("Withdrawals are not allowed on inactive accounts.");
        }

        if (account.AccountType == "Fixed Deposit" && withdrawal.Amount != account.AvailableBalance)
        {
            return BadRequest("Only 100% withdrawals are allowed on Fixed Deposit accounts.");
        }

        var success = await _repository.CreateWithdrawalAsync(withdrawal);
        if (!success) return StatusCode(500, "A problem occurred while handling your request.");

        return Ok();
    }

}
