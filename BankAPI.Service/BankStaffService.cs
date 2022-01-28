using BankAPI.IContracts;
using BankAPI.Models;

namespace BankAPI.Service
{
    public class BankStaffService : IBankStaffService
    {
        public string AddBankStaff ( string name, string password )
        {
            var user = new BankStaff()
            {
                Name = name,
                Password = password,
                Id = Utilities.GenerateUserId(name)
            };
            using ( var context = new ServiceContext() )
            {
                context.Staffs.Add(user);
                context.SaveChanges(); 
            }
            return user.Id;
        }

        public string GetUserAccount ( string id )
        {
            using ( var context = new ServiceContext() )
            {
                var user = context.Staffs.FirstOrDefault(s => s.Id == id);
                if (user == null) return null;
                else return user.Name;
            }
        }
    }
}
