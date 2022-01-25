using APIDemo.Models;

namespace APIDemo.IContracts
{
    public interface IBankStaffService
    {
        (string Id, string password) CreateAccount(string name);

        void CreateCurrency(string name, double rate);

        List<Transaction> GetTransactionHistory(int accountNumber);

        bool IsCurrencyAvailable(string name);

        void UpdateAccountStatus(int accountNumber);
    }
}
