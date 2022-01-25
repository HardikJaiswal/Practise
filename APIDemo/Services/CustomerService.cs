using APIDemo;
using APIDemo.Models;
using APIDemo.IContracts;

namespace APIDemo.Services
{
    public class CustomerService : IAccountHolderService
    {
        public void DepositMoney(int accountNumber, int amount)
        {
            if (IsAccountValid(accountNumber))
            {
                using (var context = new ApiDemoContext())
                {
                    context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber).Amount += amount;
                }
            }
        }

        public List<Transaction> GetTransactionHistory(int accountNumber)
        {
            if (IsAccountValid(accountNumber))
            {
                using (var context = new ApiDemoContext())
                {
                     return context.Transactions.Where(t=>t.SrcAcc==accountNumber || t.DestAcc==accountNumber).ToList();
                }
            }else return null;
        }

        public static bool IsAccountValid(int accountNumber)
        {
            using (var context = new ApiDemoContext())
            {
                return context.Accounts.Any(a=> a.AccountNumber == accountNumber);
            }
        }

        public bool IsAmountAvailable(int accountNumber, int amount)
        {
            if (IsAccountValid(accountNumber))
            {
                using (var context = new ApiDemoContext())
                {
                    return context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber).Amount >= amount;
                }
            }
            else return false;
        }

        public void WithdrawMoney(int accountNumber, int amount)
        {
            if(IsAmountAvailable(accountNumber, amount))
            {
                using (var context = new ApiDemoContext())
                {
                    context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber).Amount -= amount;
                }
            }
        }
    }
}
