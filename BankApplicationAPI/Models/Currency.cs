using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication.Models
{
    public class Currency
    {
        public string Name { get; set; }

        public double ConversionRate { get; set; }

        public Currency(string name,double conversionRate)
        {
            Name = name;
            ConversionRate = conversionRate;
        }
    }
}
