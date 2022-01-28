using APIDemo;
using APIDemo.Models;
using APIDemo.IContracts;
using Microsoft.EntityFrameworkCore;

namespace APIDemo.Services
{
    public class CustomerService : IAccountHolderService
    {
        public void DepositMoney(int accountNumber, int amount)
        {
            var user = GetAccount(accountNumber);
            if (user!=null)
            {
                user.Amount += amount;
                using (var context = new ApiDemoContext())
                {
                    context.Entry(user).State = EntityState.Modified;
                    Transaction transaction = new Transaction()
                    {
                        Id = Utilities.GenerateTransactionId(),
                        SrcAcc = accountNumber,
                        DestAcc = accountNumber,
                        TrasanctionType = TransactionType.Deposit,
                        Amount = amount
                    };
                    context.Entry(user).State = EntityState.Modified;
                    context.Transactions.Add(transaction);
                    context.SaveChanges();
                }
            }
        }

        public List<Transaction> GetTransactionHistory(int accountNumber)
        {
            if (GetAccount(accountNumber)!=null)
            {
                using (var context = new ApiDemoContext())
                {
                     return context.Transactions.Where(t=>t.SrcAcc==accountNumber || t.DestAcc==accountNumber).ToList();
                }
            }else return null;
        }

        public static AccountHolder GetAccount(int accountNumber)
        {
            using (var context = new ApiDemoContext())
            {
                return context.Accounts.FirstOrDefault(a=>a.AccountNumber == accountNumber);
            }
        }

        public void WithdrawMoney(int accountNumber, int amount)
        {
            var user = GetAccount(accountNumber);
            if (user != null && user.Amount >= amount)
            {
                user.Amount -= amount;
                using (var context = new ApiDemoContext())
                {
                    Transaction transaction = new Transaction()
                    {
                        Id = Utilities.GenerateTransactionId(),
                        SrcAcc = accountNumber,
                        DestAcc = accountNumber,
                        Amount = amount,
                        TrasanctionType = TransactionType.Withdrawl
                    };
                    context.Transactions.Add(transaction);
                    context.Entry(user).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        //public void TransferMoney(int srcAccNum,int destAccNum,double amount,string srcBankId,
        //    string destBankId,TransferMode mode)
        //{
        //    var user1 = GetAccount(srcAccNum);
        //    var user2 = GetAccount(destAccNum);
        //    if (user1!=null && user2!=null && user1.Amount > amount)
        //    {
        //        using (var context = new ApiDemoContext()) {
        //            var srcBank = context.Banks.FirstOrDefault(b=>b.Id == srcBankId);
        //            if (srcBank == null) return;
        //            switch (mode)
        //            {
        //                case TransferMode.IMPS:
        //                    if (srcBankId.Equals(destBankId)) user1.Amount -= ((100 + srcBank.IntraBankIMPScharges) / 100) * amount;
        //                    else user1.Amount -= ((100 + srcBank.InterBankIMPScharges) / 100) * amount;
        //                    break;
        //                case TransferMode.RTGS:
        //                    if (srcBankId.Equals(destBankId)) user1.Amount -= ((100 + srcBank.IntraBankRTGScharges) / 100) * amount;
        //                    else user1.Amount -= ((100 + srcBank.InterBankRTGScharges) / 100) * amount;
        //                    break;
        //            }
        //            user2.Amount += amount;
        //            Transaction transaction = new Transaction()
        //            {
        //                Id = Utilities.GenerateTransactionId(),
        //                SrcAcc = srcAccNum,
        //                DestAcc = destAccNum,
        //                TrasanctionType = TransactionType.Transfer,
        //                SrcBankId = srcBankId,
        //                DestBankId = destBankId,
        //                Amount = amount,
        //            };
        //            context.Transactions.Add(transaction);
        //            context.Entry(user1).State = EntityState.Modified;
        //            context.Entry(user2).State = EntityState.Modified;
        //            context.SaveChanges();
        //        }
        //    }

       // }
    }
}
