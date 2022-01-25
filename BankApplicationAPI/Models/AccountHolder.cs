using System.Collections.Generic;

namespace BankApplication.Models
{
    public class AccountHolder : User
    {
        public int AccountNumber { get; set; }

        public bool IsActive { get; set; }

        public List<Transaction> TransactionHistory { get; set; }

        public AccountHolder(int accountNumber, string name)
        {
            AccountNumber = accountNumber;
            Name = name;
            IsActive = true;
            ID = GenerateID();
            TransactionHistory = new List<Transaction>();
        }
    }
}
