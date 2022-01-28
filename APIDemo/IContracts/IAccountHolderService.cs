using APIDemo.Models;

namespace APIDemo.IContracts
{
    public interface IAccountHolderService
    {
        void DepositMoney(int accountNumber, int amount);

        //void TransferMoney(int srcAccNum, int destAccNum, double amount, string srcBankId,
        //    string destBankId, TransferMode mode);

        List<Transaction> GetTransactionHistory(int accountNumber);

        void WithdrawMoney(int accountNumber, int amount);
    }
}
