using System;
using BankAPI.Models;

namespace BankAPI.IContracts
{
    public interface ICurrencyServie
    {
        public APIResponse<string> CreateCurrency(Currency currency);

        public bool IsCurrencyAvailable(string name);
    }
}
