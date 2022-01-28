using System;
using BankAPI.IContracts;
using BankAPI.Models;

namespace BankAPI.Service
{
    public class CurrencyService : ICurrencyServie
    {

        public void CreateCurrency ( string name, double rate, string bankId )
        {
            var currency = new Currency()
            {
                Name = name,
                ValueInINR = rate,
                BankId = bankId
            };
            using ( var context = new ServiceContext() )
            {
                context.Currencies!.Add(currency);
                context.SaveChanges();
            }
        }

        public bool IsCurrencyAvailable ( string name )
        {
            using ( var context = new ServiceContext() )
            {
                return context.Currencies!.Any(t => t.Name == name);
            }
        }

    }
}
