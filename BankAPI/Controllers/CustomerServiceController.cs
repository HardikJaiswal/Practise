using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankAPI.IContracts;
using BankAPI.Service;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerServiceController : ControllerBase
    {
        IAccountHolderService _accountHolderService;

        public CustomerServiceController ( [FromServices]IAccountHolderService accountHolderService )
        {
            _accountHolderService = accountHolderService;
        }

        [HttpPost("createaccount/{name}")]
        public dynamic CreateAccountRequest ( string name )
        {
            if ( name != null )
            {
                try
                {
                    return _accountHolderService.CreateAccount(name);
                }
                catch ( Exception ex )
                {
                    return Utilities.StatusResponse(ex.Message, false);
                }
            }
            else
                return NotFound("Account Number not provided");
        }


        [HttpGet("getaccount")]
        public dynamic GetAccountRequest ( int accountNumber )
        {
            if ( accountNumber != 0 )
            {
                try
                {
                    return _accountHolderService.GetUserAccount(accountNumber);
                }
                catch ( Exception ex )
                {
                    Utilities.StatusResponse(ex.Message, false);
                }
            }
            return NotFound("Account Number not provided");
        }
    }
}
