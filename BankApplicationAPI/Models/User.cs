using System;

namespace BankApplication.Models
{
    public class User
    {
        public string ID { get; set; }

        public string Name { get; set; }
        
        public double Amount { get; set; }
        
        public char Gender { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }

        protected string GenerateID()
        {
            return $"{Name.Substring(0, 3)}{DateTime.Today}";
        }
    }
}
