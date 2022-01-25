using System.ComponentModel.DataAnnotations;

namespace APIDemo.Models
{
    public class Transaction
    {
        [Key]
        public string Id { get; set; }

        public int? SrcAcc { get; set; }

        public int? DestAcc { get; set; }

        public double Amount { get; set; }

        public TransactionType TrasanctionType { get; set; }

        public string? SrcBankId { get; set; }

        public string? DestBankId { get; set; }

        public DateTime CreatedOn
        {
            set => _ = DateTime.Now;
            get => CreatedOn;
        }
    }
}
