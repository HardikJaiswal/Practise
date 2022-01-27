using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIDemo.IContracts;
using APIDemo.Models;

namespace APIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffServiceController : ControllerBase
    {
        private IBankStaffService _staffService;

        public StaffServiceController([FromServices]IBankStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPatch("revertTransfer")]
        public IActionResult RevertTransferRequest([FromBody]Transaction transaction)
        {
            if (_staffService.IsTransferReverted(transaction))
            {
                return Ok("Transfer Reverted Successfully.");
            }
            else
            {
                return BadRequest("The transaction was not found.");
            }
        }

        [HttpPatch("updatestatus/{accountNumber}")]
        public IActionResult UpdateStatusRequest(int accountNumber)
        {
            if (accountNumber != 0)
            {
                _staffService.UpdateAccountStatus(accountNumber);
                return Ok("Withdrawl successful");
            }
            else
                return NotFound("Account Number not provided");
        }

        [HttpPost("CreateAccount/{name}")]
        public dynamic CreateAccountRequest(string name)
        {
            if (name != null)
            {
                return _staffService.CreateAccount(name);
            }
            else
                return NotFound("Account Number not provided");
        }

        [HttpPost("AddCurrency/name={name}&rate={rate}")]
        public dynamic AddCurrencyRequest(string name, double rate)
        {
            if (name != null && rate!=0 && !_staffService.IsCurrencyAvailable(name.Trim().ToLower()))
            {
                _staffService.CreateCurrency(name.Trim().ToLower(), rate);
                return Ok($"Currency:{name} with Exchange Rate to INR: Rs.{rate}");
            }
            else
                return NotFound("Complete data was not provided Or Currency is already present.");
        }

        [HttpGet("IsCurrencyAvailable/{name}")]
        public IActionResult IsCurrencyAvailableRequest(string name)
        {
            if (name != null)
            {
                return _staffService.IsCurrencyAvailable(name.Trim().ToLower()) ? 
                    Ok("This currency is available") : 
                    Ok("This currency is not available");
            }
            else
                return NotFound("Name of currency not provided.");
        }

        [HttpGet("gettransactions/{accountNumber}")]
        public dynamic GetTransactionsRequest(int accountNumber)
        {
            if (accountNumber != 0)
                return _staffService.GetTransactionHistory(accountNumber);
            else
                return NotFound("Account Number was not provided");
        }

    }
}
