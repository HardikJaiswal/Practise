
namespace BankAPI.IContracts
{
    public interface IAccountHolderService
    {
        string GetUserAccount(int accountNumber);

        (string Id, string password) CreateAccount(string name);

    }
}
