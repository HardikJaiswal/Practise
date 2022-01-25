using APIDemo.IContracts;
using APIDemo.Models;

namespace APIDemo.Services
{
    public class StaffService : IBankStaffService
    {
        public (string Id, string password) CreateAccount(string name)
        {
            int accNum = GenerateAccountNumber();
            while (CustomerService.IsAccountValid(accNum)) accNum = GenerateAccountNumber();
            AccountHolder holder = new AccountHolder()
            {
                Name = name,
                AccountNumber = accNum,
                Password = Utilities.GeneratePassword(),
                Id = Utilities.GenerateUserId(name)
            };
            using(var context = new ApiDemoContext())
            {
                context.Accounts.Add(holder);
                context.SaveChanges();
            }
            return (holder.Id, holder.Password);
        }

        public void CreateCurrency(string name, double rate)
        {
            var currency = new Currency()
            {
                Name = name,
                ValueInINR = rate
            };
            using (var context = new ApiDemoContext())
            {
                context.Currencies.Add(currency);
                context.SaveChanges();
            }
        }

        private int GenerateAccountNumber()
        {
            return ((new Random()).Next(0, 100000) % 100000) + 100000;
        }

        public List<Transaction> GetTransactionHistory(int accountNumber)
        {
            if (CustomerService.IsAccountValid(accountNumber))
            {
                using (var context = new ApiDemoContext())
                {
                    return context.Transactions.Where(t => t.SrcAcc == accountNumber || t.DestAcc == accountNumber).ToList();
                }
            }
            else return null;
        }

        public bool IsCurrencyAvailable(string name)
        {
            using(var context = new ApiDemoContext())
            {
                return (context.Currencies.Any(t => t.Name == name));
            }
        }

        public void UpdateAccountStatus(int accountNumber)
        {
            if (CustomerService.IsAccountValid(accountNumber))
            {
                using(var context = new ApiDemoContext())
                {
                    var account = context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
                    account.IsActive = !account.IsActive;
                    context.SaveChanges();
                }
            }
        }
    }
}
