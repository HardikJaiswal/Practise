using BankAPI.Models;

namespace BankAPI.Service
{
    public class Utilities
    {
        public static string GenerateUserId(string name)
        {
            return $"{name.Substring(0, 3)}{DateTime.Now.Date}";
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
                Id = $"TXN{DateTime.Now.Date}",
                Amount = amount,
                SrcAcc = srcAccountNumber,
                DestAcc = destAccountNumber,
                SrcBankId = srcBankId,
                DestBankId = destBankId,
                TrasanctionType = transactionType
            };
            return transaction;
        }

        public static APIResponse<string> StatusResponse(string message, bool isSucessful)
        {
            APIResponse<string> response = new APIResponse<string>()
            {
                IsSuccess = isSucessful,
                Message = message,
                Data = String.Empty
            };
            return response;
        }

        public static APIResponse<T> DataResponse<T>(string message, T data)
        {
            APIResponse<T> response = new APIResponse<T>()
            {
                Data = data,
                IsSuccess = true,
                Message = message
            };
            return response;
        }
    }
}
