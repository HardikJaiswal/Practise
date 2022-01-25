using System;

namespace BankApplication.Models
{
    //public enum UserServiceMenu
    //{
    //    Deposit = 1,
    //    Withdraw,
    //    Transfer,
    //    PrintTransactions,
    //    LogOut
    //}

    //public enum StartupMenu
    //{
    //    SetUpBank=1,
    //    SelectBank
    //}

    //public enum Gender
    //{
    //    Male,
    //    Female,
    //    Other
    //}

    //public enum EmployeeServiceMenu
    //{
    //    CreateAccount=1,
    //    UpdateAccountStatus,
    //    AddCurrency,
    //    ViewTransactionHistory,
    //    RevertTransaction,
    //    LogOut
    //}
    
    public enum UserType
    {
        AccountHolder=1,
        Employee
    }
    
    public enum TransactionType
    {
        Deposit,
        Withdrawl,
        Transfer
    }

    public enum GetRequestForUIServiceController
    {
        GetBankList,
        GetUserAccount
    }

    public enum PostRequestForUIServiceController
    {
        AddBank,
        AddBankStaff
    }

    public enum GetRequestForCustomerServiceController
    {
        GetTransactions
    }

    public enum GetRequestForStaffController
    {
        GetTransactionHistory,
        IsCurrencyExist
    }

    public enum PostRequestForStaffController
    {
        CreateAccount,
        AddCurrency
    }

    public enum PatchRequestForStaffController
    {
        UpdateAccountStatus
    }

    public enum PutRequestForStaffController
    {
        IsTransferReverted
    }

    public enum TransferMode
    {
        IMPS=1,
        RTGS
    }
}
