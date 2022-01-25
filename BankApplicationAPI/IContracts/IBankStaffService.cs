using BankApplication.Models;
using System;
using System.Collections.Generic;

namespace BankApplication.Contracts
{
    public interface IBankStaffService
    {
        void UpdateAccountStatus(int accountNumber);

        (string password,int accountNumber) CreateAccount(string nameOfAccountHolder);

        void AddCurrency(string name, double conversionRate);

        //bool IsCurrencyAdded(string nameOfCurrency, double conversionRateToINR);

        bool IsTransferReverted(Transaction transaction);

        bool IsCurrencyExist(string name);

        List<Transaction> GetTransactionList(int accountNumber);
    }
}
