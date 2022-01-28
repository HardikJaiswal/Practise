using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankAPI.IContracts;
using BankAPI.Models;

namespace BankAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BankServiceController : ControllerBase
    {
        IBankService _service;

        public BankServiceController ( [FromServices]IBankService service )
        {
            _service = service;
        }

        [HttpGet("gettransactions/{accountNumber}")]
        public dynamic GetTransactionsRequest( int accountNumber )
        {
            if ( accountNumber != 0 )
                return _service.GetTransactionHistory(accountNumber);
            else
                return NotFound("Account Number was not provided");
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

        [HttpPatch("Withdrawl/accountNumber={accountNumber}&amount={amount}")]
        public IActionResult MoneyWithdrawlRequest( int accountNumber, int amount )
        {
            if (accountNumber != 0)
            {
                _service.WithdrawMoney(accountNumber, amount);
                return Ok("Withdrawl successful");
            }
            else
                return NotFound("Account Number not provided");
        }

        [HttpPatch("Demposit/accountNumber={accountNumber}&amount={amount}")]
        public IActionResult MoneyDepositRequest ( int accountNumber, int amount )
        {
            if ( accountNumber != 0 )
            {
                _service.DepositMoney(accountNumber, amount);
                return Ok("Withdrawl successful");
            }
            else
                return NotFound("AccountNumber not provided");
        }

        [HttpPost("AddBank/{name}")]
        public IActionResult AddBankRequest ( string name )
        {
            if ( name == null )
            {
                return NotFound("Bank Name not provided");
            }
            else
            {
                _service.AddBank(name);
                return Ok("Bank added successfully.");
            }
        }

        [HttpPatch("revertTransfer")]
        public IActionResult RevertTransferRequest ( [FromBody] Transaction transaction )
        {
            if (_service.IsTransferReverted(transaction))
            {
                return Ok("Transfer Reverted Successfully.");
            }
            else
            {
                return BadRequest("The transaction was not found.");
            }
        }

        [HttpPatch("updatestatus/{accountNumber}")]
        public IActionResult UpdateStatusRequest ( int accountNumber )
        {
            if (accountNumber != 0)
            {
                _service.UpdateAccountStatus(accountNumber);
                return Ok("Withdrawl successful");
            }
            else
                return NotFound("Account Number not provided");
        }

        [HttpGet("GetBankList")]
        public List<string> GetBankNamesRequest()
        {
            return _service.GetBankNames();
        }

    }
}
