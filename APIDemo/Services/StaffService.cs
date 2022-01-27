using APIDemo.IContracts;
using APIDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDemo.Services
{
    public class StaffService : IBankStaffService
    {
        public (string Id, string password) CreateAccount(string name)
        {
            int accNum = GenerateAccountNumber();
            while (CustomerService.GetAccount(accNum)!=null) accNum = GenerateAccountNumber();
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
            if (CustomerService.GetAccount(accountNumber)!=null)
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
                return context.Currencies.Any(t => t.Name == name);
            }
        }

        public void UpdateAccountStatus(int accountNumber)
        {
            var user = CustomerService.GetAccount(accountNumber);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                using(var context = new ApiDemoContext())
                {
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public bool IsTransferReverted(Transaction transaction)
        {
            using(var context = new ApiDemoContext())
            {
                if(transaction != null && context.Transactions.Any(t => t.Id == transaction.Id))
                {
                    var srcUser = CustomerService.GetAccount(transaction.SrcAcc);
                    var destUser = CustomerService.GetAccount(transaction.DestAcc);
                    if(srcUser != null && destUser != null)
                    {
                        srcUser.Amount += transaction.Amount; 
                        destUser.Amount-= transaction.Amount;
                        context.Entry(srcUser).State = EntityState.Modified;
                        context.Entry(destUser).State = EntityState.Modified;
                        Transaction reverted = new Transaction()
                        {
                            Id = Utilities.GenerateTransactionId(),
                            SrcAcc = transaction.DestAcc,
                            DestAcc = transaction.SrcAcc,
                            TrasanctionType = TransactionType.RevertedTransfer
                        };
                        context.Transactions.Add(reverted);
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
