using System;

namespace BankApplication.Models
{
    public class Transaction
    {
        #region

        public string ID { get; set; }

        public int SrcAcc { get; set; }
        
        public int DescAcc { get; set; }
        
        public double Amount { get; set; }
        
        public TransactionType TransactionType { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string SenderBankID { get; set; }
        
        public string ReceiverBankID { get; set; }
        
        #endregion
    }
}
