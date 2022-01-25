using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication.Models
{
    public class BankStaff : User
    {
        public string JobRole { get; set; }

        public BankStaff(string name,string password)
        {
            Name = name;
            ID = GenerateID();
            Password = password;
        }
    }
}
