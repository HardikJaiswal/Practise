using System;
using BankAPI.Models;

namespace BankAPI.IContracts
{
    public interface ICurrencyServie
    {
        void CreateCurrency(string name, double rate, string bankId);

        public bool IsCurrencyAvailable(string name);
    }
}
