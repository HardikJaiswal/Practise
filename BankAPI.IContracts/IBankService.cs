using BankAPI.Models;

namespace BankAPI.IContracts
{
    public interface IBankService
    {
        void AddBank(string bankName);

        List<string> GetBankNames();

        void DepositMoney(int accountNumber, int amount);

        void WithdrawMoney(int accountNumber, int amount);

        List<Transaction> GetTransactionHistory(int accountNumber);

        void UpdateAccountStatus(int accountNumber);

        //void TransferMoney(int srcAccNum, int destAccNum, double amount, string srcBankId,
        //    string destBankId, TransferMode mode);

        bool IsTransferReverted(Transaction transaction);
    }
}
