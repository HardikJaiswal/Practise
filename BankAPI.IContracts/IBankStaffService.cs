using BankAPI.Models;

namespace BankAPI.IContracts
{
    public interface IBankStaffService
    {
        APIResponse<string> GetUserAccount(string id);

        APIResponse<string> AddBankStaff(string name);

    }
}
