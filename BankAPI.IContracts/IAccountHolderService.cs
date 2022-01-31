
namespace BankAPI.IContracts
{
    public interface IAccountHolderService
    {
        dynamic GetUserAccount(int accountNumber);

        (string Id, string password) CreateAccount(string name);

    }
}
