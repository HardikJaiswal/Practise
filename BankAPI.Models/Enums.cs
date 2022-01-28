namespace BankAPI.Models
{
    public enum TransactionType
    {
        Deposit,
        Withdrawl,
        Transfer,
        RevertedTransfer
    }

    public enum Gender
    {
        Male,
        FeMale,
        Transgender
    }

    public enum UserType
    {
        AccounHolder,
        BankStaff
    }

    public enum TransferMode
    {
        IMPS,
        RTGS
    }
}
