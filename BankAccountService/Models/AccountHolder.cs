using BankAccountService.Models;


public class AccountHolder
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string IDNumber { get; set; }
    public string ResidentialAddress { get; set; }
    public string MobileNumber { get; set; }
    public string Email { get; set; }
    public List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>(); // Initialize here


}
