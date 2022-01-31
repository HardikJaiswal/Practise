using BankAPI.IContracts;
using BankAPI.Models;

namespace BankAPI.Service
{
    public class CurrencyService : ICurrencyServie
    {
        private readonly ServiceContext _context;

        public CurrencyService ( ServiceContext serviceContext ) 
        {
            _context = serviceContext; 
        }

        public APIResponse<string> CreateCurrency ( Currency currency )
        {
            _context.Currencies!.Add(currency);
            _context.SaveChanges();
            return Utilities.
                StatusResponse($"Currency:{currency.Name} with Exchange Rate to INR: Rs.{currency.ValueInINR}", true);
        }

        public bool IsCurrencyAvailable ( string name )
        {
            return _context.Currencies!.Any(t => t.Name == name);    
        }

    }
}
