using BankApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication.Contracts
{
    public interface IAccountHolderService
    {
        List<Transaction> GetTransactionHistory(int accountNumber);
        void DepositMoney(int accountNumber, double moneyToBeDeposited);

        void TransferMoney(int senderAccountNumber, int receiverAccountNumber, double amountToBeTransffered,
             TransferMode choice);

        void WithdrawMoney(int accountNumber, double moneyToBeWithdrawl);

    }
}
