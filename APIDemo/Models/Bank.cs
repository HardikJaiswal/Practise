using System.ComponentModel.DataAnnotations;

namespace APIDemo.Models
{
    public class Bank
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string? Address { get; set; }

        public string? IFSC { get; set; }

        public double IntraBankRTGScharges 
        { 
            get => IntraBankRTGScharges;
            set => IntraBankRTGScharges = 0;
        }

        public double IntraBankIMPScharges 
        {
            get => IntraBankIMPScharges;
            set => IntraBankIMPScharges = 5;
        }

        public double InterBankIMPScharges
        {
            get => InterBankIMPScharges;
            set => InterBankIMPScharges = 6;
        }

        public double InterBankRTGScharges
        {
            get => InterBankRTGScharges;
            set => InterBankRTGScharges = 2;
        }
    }
}
