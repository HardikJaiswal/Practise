using BankAPI.Models;

namespace BankAPI.Service
{
    public class Utilities
    {
        public static string GenerateUserId(string name)
        {
            return $"{name.Substring(0, 3)}{DateTime.Now}";
        }

        public static string GenerateTransactionId()
        {
            return $"TXN{DateTime.Now}";
        }

        public static string GeneratePassword()
        {
            return $"PWD{DateTime.Now.Millisecond}";
        }

        public static int GenerateAccountNumber()
        {
            return ((new Random()).Next(0, 100000) % 100000) + 100000;
        }

        public static Transaction CreateTransaction(TransactionType transactionType,int srcAccountNumber, 
            int destAccountNumber, double amount,string? srcBankId, string? destBankId)
        {
            Transaction transaction = new Transaction()
            {
                Id = GenerateTransactionId(),
                Amount = amount,
                SrcAcc = srcAccountNumber,
                DestAcc = destAccountNumber,
                SrcBankId = srcBankId,
                DestBankId = destBankId,
                TrasanctionType = transactionType
            };
            return transaction;
        }
    }
}
