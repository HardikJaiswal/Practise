using BankAPI.IContracts;
using BankAPI.Models;

namespace BankAPI.Service
{
    public class AccountHolderService : IAccountHolderService
    {
        public ( string Id, string password ) CreateAccount( string name )
        {
            var user = new AccountHolder()
            {
                Name = name,
                Password = Utilities.GeneratePassword(),
                Id = Utilities.GeneratePassword(),
                Amount = 0,
                IsActive = true
            };
            using ( var context = new ServiceContext() )
            {
                int accNum = Utilities.GenerateAccountNumber();
                while ( context.Accounts!.Any(a => a.AccountNumber == accNum) ) accNum = Utilities.GenerateAccountNumber();
                user.AccountNumber = accNum;
                context.Accounts!.Add( user );
                context.SaveChanges();
            }
            return (user.Id, user.Password );
        }

        public static bool IsAccountPresent ( int accountNumber )
        {
            using ( var context = new ServiceContext() )
                return context.Accounts!.Any(a => a.AccountNumber == accountNumber);
        }

        public string GetUserAccount ( int accountNumber )
        {
            using( var context = new ServiceContext())
            {
                var user = context.Accounts!.FirstOrDefault( a => a.AccountNumber == accountNumber );
                if( user == null ) return String.Empty;
                return user.Name;
            }
        }
    }
}
