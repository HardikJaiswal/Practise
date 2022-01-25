using APIDemo.Models;

namespace APIDemo.IContracts
{
    public interface IAccountHolderService
    {
        void DepositMoney(int accountNumber, int amount);

        List<Transaction> GetTransactionHistory(int accountNumber);

        bool IsAmountAvailable(int accountNumber, int amount);

        void WithdrawMoney(int accountNumber, int amount);
    }
}
