using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIDemo.IContracts;

namespace APIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerServiceController : ControllerBase
    {
        IAccountHolderService _accountHolderService;

        public CustomerServiceController([FromServices]IAccountHolderService accountHolderService)
        {
            _accountHolderService = accountHolderService;
        }

        [HttpGet("GetTransactions/{accountNumber}")]
        public dynamic GetTransactionsRequest(int accountNumber)
        {
            if (accountNumber != 0)
                return _accountHolderService.GetTransactionHistory(accountNumber);
            else
                return NotFound("Account Number was not provided");
        }

        [HttpPatch("Withdrawl/accountNumber={accountNumber}&amount={amount}")]
        public IActionResult MoneyWithdrawlRequest(int accountNumber,int amount)
        {
            if (accountNumber != 0)
            {
                _accountHolderService.WithdrawMoney(accountNumber, amount);
                return Ok("Withdrawl successful");  
            }
            else
                return NotFound("Account Number not provided");
        }

        [HttpPatch("Demposit/accountNumber={accountNumber}&amount={amount}")]
        public IActionResult MoneyDepositRequest(int accountNumber, int amount)
        {
            if (accountNumber != 0 && _accountHolderService.IsAmountAvailable(accountNumber,amount))
            {
                _accountHolderService.DepositMoney(accountNumber, amount);
                return Ok("Withdrawl successful");
            }
            else
                return NotFound("AccountNumber not provided");
        }

    }
}
