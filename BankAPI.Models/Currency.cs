namespace BankAPI.Models
{
    public class Currency
    {
        public string Name { get; set;}

        public double ValueInINR { get; set; }

        public string? BankId { get; set; }
    }
}
