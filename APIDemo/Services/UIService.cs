using APIDemo.IContracts;
using APIDemo.Models;

namespace APIDemo.Services
{
    public class UIService : IUIService
    {
        public void AddBank(string bankName)
        {
            var bank = new Bank()
            {
                Name = bankName,
                Id = Utilities.GenerateUserId(bankName)
            };
            using(var context = new ApiDemoContext())
            {
                context.Banks.Add(bank);
                context.SaveChanges();
            }
        }

        public string AddBankStaff(string name, string password)
        {
            var bankstaff = new BankStaff()
            {
                Name=name,
                Password=password,
                Id = Utilities.GenerateUserId(name)
            };
            using (var context = new ApiDemoContext())
            {
                context.Staffs.Add(bankstaff);
                context.SaveChanges();
            }
            return bankstaff.Id;    
        }

        public List<string> GetBankNames()
        {
            using (var context = new ApiDemoContext())
            {
                return context.Banks.Select(b => b.Name).ToList();
            }
        }

        public string GetUserAccount(string id, UserType userType)
        {
            switch (userType)
            {
                case UserType.AccounHolder:
                    using (var context = new ApiDemoContext())
                    {
                        return context.Accounts.FirstOrDefault(a => a.Id == id).Name;
                    }
                case UserType.BankStaff:
                    using (var context = new ApiDemoContext())
                    {
                        return context.Staffs.FirstOrDefault(a => a.Id == id).Name;
                    }
                default: return null;
            }
        }
    }
}
