using BankAPI.IContracts;
using BankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Service
{
    public class BankService : IBankService
    {
        private readonly ServiceContext _context;

        public BankService(ServiceContext serviceContext)
        {
            _context = serviceContext;
        }

        public APIResponse<string> AddBank ( string bankName )
        {
            if (_context.Banks.Any(b => b.Name == bankName.Trim().ToLower()))
                return Utilities.StatusResponse("Bank Already Available", false);

            var bank = new Bank()
            {
                Name = bankName.Trim().ToLower(),
                Id = Utilities.GenerateUserId(bankName)
            };
            _context.Banks!.Add(bank);
            _context.SaveChanges();
            return Utilities.StatusResponse("Bank Added Successfully", true);
        }

        public APIResponse<string> DepositMoney ( int accountNumber, int amount )
        {
            var user = _context.Accounts!.FirstOrDefault(a=>a.AccountNumber==accountNumber);
            if (user == null)
            {
                return Utilities.StatusResponse("Invalid Account Number", false);
            }
            
            user.Amount += amount;
            _context.Entry(user).State = EntityState.Modified;
            Transaction transaction = Utilities.CreateTransaction(TransactionType.Deposit, accountNumber,
                accountNumber, amount, null, null);
            _context.Transactions!.Add(transaction);
            _context.SaveChanges();
            return Utilities.StatusResponse("Money deposited successfully", true);
        }

        public dynamic GetBankNames()
        {
            var result = _context.Banks!.Select(b => b.Name).ToList();   
            return Utilities.DataResponse<List<string>>("List of Banks", result);
        }

        public dynamic GetTransactionHistory( int accountNumber )
        {
            var user = _context.Accounts!.FirstOrDefault(a => a.AccountNumber==accountNumber);
            if ( user == null)
            {
                return Utilities.StatusResponse("Invalid Account Number", false);
            }

            var result = _context.Transactions!.
                    Where(t => t.SrcAcc == accountNumber || t.DestAcc == accountNumber).ToList();
            return Utilities.DataResponse("Transaction history", result);
        }

        public APIResponse<string> RevertTransfer(Transaction transaction)
        {
            var srcUser = _context.Accounts!.FirstOrDefault(a => a.AccountNumber == transaction.SrcAcc);
            var destUser = _context.Accounts!.FirstOrDefault(a => a.AccountNumber == transaction.DestAcc);
            if (transaction == null || !_context.Transactions!.Any(t => t.Id == transaction.Id) || srcUser == null
                || destUser == null)
            {
                return Utilities.StatusResponse("Invalid transaction", false);
            }

            srcUser.Amount += transaction.Amount;
            destUser.Amount -= transaction.Amount;
            _context.Entry(srcUser).State = EntityState.Modified;
            _context.Entry(destUser).State = EntityState.Modified;
            Transaction reverted = Utilities.CreateTransaction(TransactionType.RevertedTransfer,
                transaction.DestAcc, transaction.SrcAcc, transaction.Amount, null, null);
            _context.Transactions!.Add(reverted);
            _context.SaveChanges();
            return Utilities.StatusResponse("Transfer Reverted", true);
        }

        public APIResponse<string> TransferMoney(int srcAccNum, int destAccNum, double amount, string srcBankId,
            string destBankId, TransferMode mode)
        {
            var user1 = _context.Accounts!.FirstOrDefault(a=> a.AccountNumber == srcAccNum);
            var user2 = _context.Accounts!.FirstOrDefault(a => a.AccountNumber == destAccNum);
            var srcBank = _context.Banks!.FirstOrDefault(b => b.Id == srcBankId);
            if (srcBank == null || user1 == null || user2 == null || user1.Amount <= amount)
            {
                return Utilities.StatusResponse("Invalid Information", false);
            }

            switch (mode)
            {
                case TransferMode.IMPS:
                    if (srcBankId.Equals(destBankId)) user1.Amount -= ((100 + srcBank.IntraBankIMPScharges) / 100) * amount;
                    else user1.Amount -= ((100 + srcBank.InterBankIMPScharges) / 100) * amount;
                    break;
                case TransferMode.RTGS:
                    if (srcBankId.Equals(destBankId)) user1.Amount -= ((100 + srcBank.IntraBankRTGScharges) / 100) * amount;
                    else user1.Amount -= ((100 + srcBank.InterBankRTGScharges) / 100) * amount;
                    break;
            }
            user2.Amount += amount;
            Transaction transaction = Utilities.CreateTransaction(TransactionType.Transfer, srcAccNum,
                destAccNum, amount, srcBankId, destBankId);
            _context.Transactions!.Add(transaction);
            _context.Entry(user1).State = EntityState.Modified;
            _context.Entry(user2).State = EntityState.Modified;
            _context.SaveChanges();
            return Utilities.StatusResponse("Money transfer successfull", true);
        }

        public APIResponse<string> UpdateAccountStatus ( int accountNumber )
        {
            var user = _context.Accounts!.FirstOrDefault(t => t.AccountNumber == accountNumber);
            if (user != null)
            {
                return Utilities.StatusResponse("Invalid Account Number", false);
            }

            user!.IsActive = !user.IsActive;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return Utilities.StatusResponse("Account Status Updated", true);
        }

        public APIResponse<string> WithdrawMoney ( int accountNumber, int amount )
        {
            var user = _context.Accounts!.FirstOrDefault(a=>a.AccountNumber==accountNumber);
            if (user == null && user!.Amount < amount)
            {
                return Utilities.StatusResponse("Invalid account number or insufficient balance", false);
            }
            
            user!.Amount -= amount;
            Transaction transaction = Utilities.CreateTransaction(TransactionType.Withdrawl, accountNumber,
                accountNumber, amount, null, null);
            _context.Transactions!.Add(transaction);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return Utilities.StatusResponse("Money Withdrawl Successful", true);
        }
    }
}
