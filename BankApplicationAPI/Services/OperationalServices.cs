using BankApplication.Models;
using System.Data;

namespace BankApplication.Services
{

    public class OperationalServices /*: IOperationalService*/
    {
        private List<Bank> _banks;

        private Bank _currentBank;

        public OperationalServices()
        {
            _banks = new List<Bank>();
        }

        public void AddBankStaff(string name, string password)
        {
            BankStaff newEmployee = new BankStaff(name, password);
            _currentBank.EmployeeAccounts.Add(newEmployee);
        }

        public bool IsBankAdded(string bankName)
        {
            if (GetBank(bankName) == null)
            {
                Bank newBank = new Bank(bankName);
                _banks.Add(newBank);
                _currentBank = newBank;
                return true;
            }
            return false;
        }

        private AccountHolder GetAccountHolder(int accountNumeber, ref Bank target )
        {
            return target.UserAccounts.FirstOrDefault(a => a.AccountNumber == accountNumeber);
        }

        private BankStaff GetBankStaff(string ID,ref Bank target)
        {
            return target.EmployeeAccounts.FirstOrDefault(e => e.ID == ID);
        }

        public bool IsAccountValid(int accountNumber, string password, UserType usertype,string id)
        {
            switch (usertype)
            {
                case UserType.AccountHolder:
                    return !GetAccountHolder(accountNumber,ref _currentBank).IsActive ||
                        GetAccountHolder(accountNumber, ref _currentBank).Password == password;
                case UserType.Employee:
                    return !IsAccountExist(0,id) ||
                        GetBankStaff(id,ref _currentBank).Password != password;
                default:
                    return false;
            }
        }

        public string GetUserAccount(string id, UserType usertype)
        {
            switch (usertype)
            {
                case UserType.AccountHolder:
                    return GetAccountHolder(int.Parse(id), ref _currentBank).Name;
                case UserType.Employee:
                    return GetBankStaff(id,ref _currentBank).Name;
                default: return "";
            }
        }

        public bool IsAmountAvailabe(int accountNumber, double amount)
        {
            return GetAccountHolder(accountNumber, ref _currentBank).Amount > amount;
        }

        public Bank GetBank(string id)
        {
            return _banks.FirstOrDefault(b => b.BankID == id);
        }

        public List<string> GetBankList()
        {
            List<string> result = _banks.Select(b => b.Name + "\t\t" + b.BankID).ToList();
            return result;
        }


        public bool IsBankAvailable(string bankID)
        {
            _currentBank = GetBank(bankID);
            return _currentBank != null;
        }

        public List<Transaction> GetTransactions(int accountNumber)
        {
            return GetAccountHolder(accountNumber, ref _currentBank).TransactionHistory;
        }

        public void DepositMoney(int accountNumber, double moneyToBeDeposited)
        {
            GetAccountHolder(accountNumber, ref _currentBank).Amount += moneyToBeDeposited;
            _currentBank.Transactions.Add(CreateTransaction(moneyToBeDeposited, TransactionType.Deposit, _currentBank.BankID,
                GetAccountHolder(accountNumber, ref _currentBank)));
        }

        private string GenerateTransactionID(string bankID, string userID)
        {
            return $"TXN{bankID}{userID}{DateTime.Today}";
        }

        private Transaction CreateTransaction(double amount, TransactionType typeOfTransaction, string senderBankID,
            AccountHolder sender, AccountHolder receiver = null, string receiverBankID = "", bool isReceiver = false)
        {
            string transactionID = !isReceiver ? GenerateTransactionID(senderBankID, sender.ID) :
                GenerateTransactionID(receiverBankID, receiver.ID);
            Transaction currentTransaction = new Transaction(transactionID, amount, typeOfTransaction);
            switch (typeOfTransaction)
            {
                case TransactionType.Deposit:
                    currentTransaction.ReceiverAccountNumber = sender.AccountNumber;
                    currentTransaction.ReceiverBankID = senderBankID;
                    break;
                case TransactionType.Withdrawl:
                    currentTransaction.SenderAccountNumber = sender.AccountNumber;
                    currentTransaction.SenderBankID = senderBankID;
                    break;
                case TransactionType.Transfer:
                    currentTransaction.SenderAccountNumber = sender.AccountNumber;
                    currentTransaction.SenderBankID = senderBankID;
                    currentTransaction.ReceiverAccountNumber = receiver.AccountNumber;
                    currentTransaction.ReceiverBankID = receiverBankID;
                    break;
            }
            if (!isReceiver)
            {
                sender.TransactionHistory.Add(currentTransaction);
            }
            else
            {
                receiver.TransactionHistory.Add(currentTransaction);
            }
            return currentTransaction;
        }

        //public void TransferMoney(int senderAccountNumber, int receiverAccountNumber, double amountToBeTransffered,
        //    string beneficiarysBankID, TransferMode choice)
        //{
        //    beneficiarysBankID.Transactions.Add(CreateTransaction(amountToBeTransffered, TransactionType.Transfer,
        //        _currentBank.BankID, GetAccountHolder(senderAccountNumber, ref _currentBank),
        //        GetAccountHolder(receiverAccountNumber,ref beneficiarysBankID), beneficiarysBankID.BankID, true));
        //    double taxValue = 1;
        //    switch (choice)
        //    {
        //        case TransferMode.RTGS:
        //            taxValue += (_currentBank!=beneficiarysBankID?_currentBank.InterBankRTGScharges:_currentBank.IntraBankRTGScharges)/100;
        //            break;
        //        case TransferMode.IMPS:
        //            taxValue += (_currentBank!=beneficiarysBankID?_currentBank.InterBankIMPScharges:_currentBank.IntraBankIMPScharges)/100;
        //            break;
        //    }
        //    GetAccountHolder(senderAccountNumber, ref _currentBank).Amount -= taxValue * amountToBeTransffered;
        //    _currentBank.Transactions.Add(CreateTransaction(taxValue * amountToBeTransffered, TransactionType.Transfer,
        //        _currentBank.BankID, GetAccountHolder(senderAccountNumber, ref _currentBank),
        //        GetAccountHolder(receiverAccountNumber, ref beneficiarysBankID), beneficiarysBankID.BankID));

        //}

        public void WithdrawMoney(int accountNumber, double moneyToBeWithdrawl)
        {
            AccountHolder user = GetAccountHolder(accountNumber, ref _currentBank);
            user.Amount -= moneyToBeWithdrawl;
            _currentBank.Transactions.Add(CreateTransaction(moneyToBeWithdrawl, TransactionType.Withdrawl, _currentBank.BankID,
                _currentBank.UserAccounts[accountNumber]));
        }

        private void RequestTransferFromAnotherBank(string bankID, int accountNumber, double amount)
        {
            Bank target = GetBank(bankID);
            GetAccountHolder(accountNumber, ref target).Amount += amount;
        }

        public bool IsAccountExist(int accountNumber=0,string ID="")
        {
            return ID!=""? _currentBank.UserAccounts.Any(a => a.AccountNumber==accountNumber):
                _currentBank.EmployeeAccounts.Any(b => b.ID==ID);
        }

        public void UpdateAccountStatus(int accountNumber)
        {
            GetAccountHolder(accountNumber, ref _currentBank).IsActive = !GetAccountHolder(accountNumber, ref _currentBank).IsActive;
        }

        public List<Transaction> GetTransactionHistory(int accountNumber)
        {
            return GetAccountHolder(accountNumber, ref _currentBank).TransactionHistory;
        }

        public (string password, int accountNumber) CreateAccount(string nameOfAccountHolder)
        {
            int accountNumber = GenerateAccountNumber();
            while (IsAccountExist(accountNumber)) accountNumber = GenerateAccountNumber();
            _currentBank.UserAccounts.Add(new AccountHolder(accountNumber, nameOfAccountHolder));
            //Random format to generate password.
            string password = $"{nameOfAccountHolder.Substring(0, 3)}{accountNumber % 1000}";
            return (password, accountNumber);
        }

        private int GenerateAccountNumber()
        {
            return ((new Random()).Next(0, 100000) % 100000) + 100000;
        }

        public bool IsCurrencyAdded(string nameOfCurrency, double conversionRateToINR)
        {
            if (!_currentBank.Currency.Any(c => c.Name==nameOfCurrency))
            {
                _currentBank.Currency.Add(new Currency(nameOfCurrency,conversionRateToINR));
                return true;
            }
            return false;
        }

        public bool IsTransferReverted(Transaction transaction)
        {
            if (transaction.TransactionType == TransactionType.Transfer)
            {
                //same bank 
                if (_currentBank.BankID == transaction.SenderBankID && _currentBank.BankID == transaction.ReceiverBankID)
                {
                    GetAccountHolder(transaction.SenderAccountNumber, ref _currentBank).Amount += transaction.Amount;
                    GetAccountHolder(transaction.ReceiverAccountNumber,ref _currentBank).Amount -= transaction.Amount;
                }
                //different bank
                else
                {
                    if (_currentBank.BankID == transaction.SenderBankID)
                    {
                        GetAccountHolder(transaction.SenderAccountNumber, ref _currentBank).Amount += transaction.Amount;
                        RequestTransferFromAnotherBank(transaction.ReceiverBankID, transaction.ReceiverAccountNumber,
                            -1 * transaction.Amount); // -1 for deducting the amount.
                    }
                    else
                    {
                        RequestTransferFromAnotherBank(transaction.SenderBankID, transaction.SenderAccountNumber,
                            transaction.Amount);
                        GetAccountHolder(transaction.ReceiverAccountNumber, ref _currentBank).Amount -= transaction.Amount;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
