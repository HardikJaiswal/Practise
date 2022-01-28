using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankAPI.IContracts;

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

        [HttpPost("CreateAccount/{name}")]
        public dynamic CreateAccountRequest ( string name )
        {
            if ( name != null )
            {
                return _accountHolderService.CreateAccount(name);
            }
            else
                return NotFound("Account Number not provided");
        }

    }
}
