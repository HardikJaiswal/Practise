using BankAPI.Models;

namespace BankAPI.IContracts
{
    public interface IBankService
    {
        APIResponse<string> AddBank(string bankName);

        dynamic GetBankNames();

        APIResponse<string> DepositMoney(int accountNumber, int amount);

        APIResponse<string> WithdrawMoney(int accountNumber, int amount);

        dynamic GetTransactionHistory(int accountNumber);

        APIResponse<string> UpdateAccountStatus(int accountNumber);

        APIResponse<string> TransferMoney(int srcAccNum, int destAccNum, double amount, string srcBankId,
            string destBankId, TransferMode mode);

        APIResponse<string> RevertTransfer(Transaction transaction);
    }
}
