
namespace BankAPI.IContracts
{
    public interface IBankStaffService
    {
        string GetUserAccount(string id);

        string AddBankStaff(string name, string password);

    }
}
