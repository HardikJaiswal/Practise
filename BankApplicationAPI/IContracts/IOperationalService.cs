using System.Collections.Generic;
using BankApplication.Models;

namespace BankApplication.Contracts
{
    public interface IOperationalService 
    {
        void AddBank(string bankName);

        bool IsBankNameExist(string bankName);

        bool IsAccountValid(string id, string password, UserType usertype);

        string GetUserAccount(string id, UserType usertype);

        bool IsAccountExist(int accountNumber);

        List<string> GetBankList();

        bool IsBankAvailable(string bank);

        //Bank GetBank(string id);

        void AddBankStaff(string name,string password);

        //bool IsBankAdded(string bankName);

        bool IsAmountAvailabe(int accountNumber, double amount);
    }
}
