using BankAPI.IContracts;
using BankAPI.Models;

namespace BankAPI.Service
{
    public class AccountHolderService : IAccountHolderService
    {
        private readonly ServiceContext _context;

        public AccountHolderService(ServiceContext serviceContext)
        {
            _context = serviceContext;
        }

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
            
            int accNum = Utilities.GenerateAccountNumber();
            while ( _context.Accounts!.Any(a => a.AccountNumber == accNum) ) accNum = Utilities.GenerateAccountNumber();
            user.AccountNumber = accNum;
            _context.Accounts!.Add( user );
            _context.SaveChanges();
            
            return (user.Id, user.Password );
        }

        public bool IsAccountPresent ( int accountNumber )
        {
            return _context.Accounts!.Any(a => a.AccountNumber == accountNumber);
        }

        public dynamic GetUserAccount ( int accountNumber )
        {
            var user = _context.Accounts!.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (user == null) return Utilities.StatusResponse("Invalid Account Number", false);

            return Utilities.DataResponse<string>("User Name", user.Name);           
        }
    }
}
