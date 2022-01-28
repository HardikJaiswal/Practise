using BankAPI.IContracts;
using BankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Service
{
    public class BankService : IBankService
    {
        public void AddBank ( string bankName )
        {
            var bank = new Bank()
            {
                Name = bankName,
                Id = Utilities.GenerateUserId(bankName)
            };
            using ( var context = new ServiceContext() )
            {
                context.Banks!.Add(bank);
                context.SaveChanges();
            }
        }

        public void DepositMoney ( int accountNumber, int amount )
        {
            using ( var context = new ServiceContext() )
            {
                var user = context.Accounts!.FirstOrDefault(a=>a.AccountNumber==accountNumber);
                if ( user == null ) return;
                user.Amount += amount;
                context.Entry(user).State = EntityState.Modified;
                Transaction transaction = Utilities.CreateTransaction(TransactionType.Deposit, accountNumber,
                    accountNumber, amount, null, null);
                context.Entry(user).State = EntityState.Modified;
                context.Transactions!.Add(transaction);
                context.SaveChanges();
            }
        }

        public List<string> GetBankNames()
        {
            using ( var context = new ServiceContext() )
            {
                return context.Banks!.Select(b => b.Name).ToList();
            }
        }

        public List<Transaction> GetTransactionHistory( int accountNumber )
        {
            if ( AccountHolderService.IsAccountPresent(accountNumber) )
            {
                using ( var context = new ServiceContext() )
                {
                    return context.Transactions!.Where(t => t.SrcAcc == accountNumber || t.DestAcc == accountNumber).ToList();
                }
            }
            else return new List<Transaction>();
        }

        public bool IsTransferReverted ( Transaction transaction )
        {
            using ( var context = new ServiceContext() )
            {
                if ( transaction != null && context.Transactions!.Any(t => t.Id == transaction.Id) )
                {
                    var srcUser = context.Accounts!.FirstOrDefault( a => a.AccountNumber==transaction.SrcAcc);
                    var destUser = context.Accounts!.FirstOrDefault(a => a.AccountNumber == transaction.DestAcc);
                    if ( srcUser != null && destUser != null )
                    {
                        srcUser.Amount += transaction.Amount;
                        destUser.Amount -= transaction.Amount;
                        context.Entry(srcUser).State = EntityState.Modified;
                        context.Entry(destUser).State = EntityState.Modified;
                        Transaction reverted = Utilities.CreateTransaction(TransactionType.RevertedTransfer,
                            transaction.DestAcc,transaction.SrcAcc,transaction.Amount,null,null);
                        context.Transactions!.Add(reverted);
                        context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
        }

        public void UpdateAccountStatus ( int accountNumber )
        {
            if ( AccountHolderService.IsAccountPresent(accountNumber) )
            {
                using ( var context = new ServiceContext() )
                {
                    var user = context.Accounts!.FirstOrDefault(t => t.AccountNumber == accountNumber);
                    user!.IsActive = !user.IsActive;
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void WithdrawMoney ( int accountNumber, int amount )
        {
            using ( var context = new ServiceContext() )
            {
                var user = context.Accounts!.FirstOrDefault(a=>a.AccountNumber==accountNumber);
                if (user == null) return;
                user!.Amount -= amount;
                Transaction transaction = Utilities.CreateTransaction(TransactionType.Withdrawl, accountNumber,
                    accountNumber, amount, null, null);
                context.Transactions!.Add(transaction);
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
