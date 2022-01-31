using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankAPI.IContracts;
using BankAPI.Service;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffServiceController : ControllerBase
    {
        private IBankStaffService _staffService;

        public StaffServiceController ( [FromServices]IBankStaffService staffService )
        {
            _staffService = staffService;
        }

        [HttpPost("addBank/{name}")]
        public dynamic AddBankStaffRequest ( string name)
        {
            if ( name == String.Empty)
            {
                return NotFound("Name not provided");
            }
            else
            {
                try
                {
                    return _staffService.AddBankStaff(name);
                }
                catch ( Exception ex )
                {
                    return Utilities.StatusResponse(ex.Message, false);
                }
            }
        }

        [HttpGet("getuseraccount/{id}")]
        public dynamic GetUserAccountRequest ( string id )
        {
            if ( id == string.Empty ) return NotFound("Id not found");
            try
            {
                return _staffService.GetUserAccount(id);
            }
            catch ( Exception ex )
            {
                return Utilities.StatusResponse (ex.Message, false);
            }
        }

    }
}
