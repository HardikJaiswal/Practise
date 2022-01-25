using System.ComponentModel.DataAnnotations;

namespace APIDemo.Models
{
    public class AccountHolder : User
    {
        
        public int Amount { get; set; }

        public int AccountNumber { get; set; }

        public bool IsActive
        {
            get => IsActive;
            set => IsActive = value;
        }

    }
}
