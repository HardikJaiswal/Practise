using APIDemo.Models;

namespace APIDemo.IContracts
{
    public interface IUIService
    {
        void AddBank(string bankName);

        string AddBankStaff(string name,string password);

        List<string> GetBankNames();

        string GetUserAccount(string id, UserType userType);
    }
}
