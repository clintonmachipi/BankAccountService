namespace BankAccountService.Models
{
    public class BankAccount
    {
        public int Id { get; set; } = 1;
        public string AccountNumber { get; set; } = "01";
        public string AccountType { get; set; } = "savings";
        public string Name { get; set; } = "clinton";
        public string Status { get; set; } = "Active";
        public decimal AvailableBalance { get; set; } = 1000;
        public int AccountHolderId { get; set; } = 22;
        public AccountHolder AccountHolder { get; set; }
        public List<Withdrawal> Withdrawals { get; set; }
    }
}
