using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIDemo.IContracts;
using APIDemo.Models;

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
            if (accountNumber != 0 )
            {
                _accountHolderService.DepositMoney(accountNumber, amount);
                return Ok("Withdrawl successful");
            }
            else
                return NotFound("AccountNumber not provided");
        }

        //[HttpPatch("TranferMoney")]
        //public IActionResult MoneyTransferRequest([FromBody] int srcAcc,[FromBody] int destAcc,[FromBody] string srcBankId
        //    ,[FromBody] string destBankId,[FromBody] TransferMode mode,[FromBody] double amount)
        //{
        //    if(srcAcc != 0 && destAcc!=0 && srcBankId!=null && destBankId!=null && amount != 0)
        //    {
        //        _accountHolderService.TransferMoney(srcAcc, destAcc, amount, srcBankId, destBankId, mode);
        //        return Ok("Money transferred successfully");
        //    }
        //    else
        //    {
        //        return BadRequest("Parameters were not provided correctly.");
        //    }
        //}
    }
}
