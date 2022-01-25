using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApplication;
using BankApplication.Models;
using BankApplication.Contracts;

namespace BankApplicationAPI.Controllers
{
    [Route("api/customerservice")]
    [ApiController]
    public class CustomerServiceController : ControllerBase
    {
        private int _accountNumber;

        IAccountHolderService _accountHolderService;

        public CustomerServiceController(IAccountHolderService accountHolderService)
        {
            _accountHolderService = accountHolderService;
        }

        public List<Transaction> GetAlltransactions()
        {
            return new List<Transaction>();
        }
        
        [HttpGet]
        public dynamic GetRequest(GetRequestForCustomerServiceController getTypeForCustomerServiceController)
        {
            switch (getTypeForCustomerServiceController)
            {
                case GetRequestForCustomerServiceController.GetTransactions:
                    return _accountHolderService.GetTransactionHistory(_accountNumber);
                default:
                    return NotFound("No such type of request.");
            }
        }

        [HttpPatch]
        public IActionResult PatchRequest(TransactionType transactionType)
        {
            switch (transactionType)
            {
                case TransactionType.Transfer:
                    _accountHolderService.TransferMoney(_accountNumber,1313513,45, TransferMode.IMPS);
                    return Ok("Transfer was successful");
                case TransactionType.Withdrawl:
                    _accountHolderService.WithdrawMoney(_accountNumber,45);
                    return Ok("Withdrawl successful");
                case TransactionType.Deposit:
                    _accountHolderService.DepositMoney(_accountNumber, 45);
                    return Ok("Deposit Successful");
                default:
                    return NotFound("No such type of request");
            }
        }
    }
}
