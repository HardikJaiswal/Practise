using System.Collections.Generic;

namespace BankApplication.Models
{
    public class Bank
    {
        public List<AccountHolder> UserAccounts { get; set; }
        
        public List<BankStaff> EmployeeAccounts { get; set; }
        
        public List<Currency> Currency { get; set; }
        
        public string Name { get; set; }

        public double Amount { get; set; }

        public Currency DefaultCurrency { get; set; }

        public double IntraBankRTGScharges { get; set; }

        public double IntraBankIMPScharges { get; set; }

        public double InterBankRTGScharges { get; set; }

        public double InterBankIMPScharges { get; set; }

        public string IFSC { get; set; }
        
        public List<Transaction> Transactions { get; set; }
        
        public string BankID { get; set; }

        public Bank(string name)
        {
            Name = name;
            Amount = 0;
            IFSC = "";
            BankID = $"{Name.Substring(0, 3)}{System.DateTime.Today}";
            Transactions = new List<Transaction>();
            UserAccounts = new List<AccountHolder>();
            EmployeeAccounts = new List<BankStaff>();
            Currency = new List<Currency>();
            DefaultCurrency = new Currency("INR", 1);
            IntraBankRTGScharges = 0;
            IntraBankIMPScharges = 5;
            InterBankRTGScharges = 2;
            InterBankIMPScharges = 6;
        }
    }
}