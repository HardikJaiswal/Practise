using BankAPI.IContracts;
using BankAPI.Models;

namespace BankAPI.Service
{
    public class BankStaffService : IBankStaffService
    {
        private readonly ServiceContext _context;

        public BankStaffService(ServiceContext serviceContext)
        {
            _context = serviceContext;
        }

        public APIResponse<string> AddBankStaff ( string name )
        {
            var user = new BankStaff()
            {
                Name = name,
                Password = Utilities.GeneratePassword(),
                Id = Utilities.GenerateUserId(name)
            };
            _context.Employees.Add(user);
            _context.SaveChanges();
            return Utilities.DataResponse<string>("User ID", user.Id);
        }

        public APIResponse<string> GetUserAccount ( string id )
        {
            var user = _context.Employees.FirstOrDefault(s => s.Id == id);
            if (user == null)
            {
                return Utilities.StatusResponse("Invalid user ID", false);
            }
            else return Utilities.DataResponse("User Name", user.Name);
        }
    }
}
