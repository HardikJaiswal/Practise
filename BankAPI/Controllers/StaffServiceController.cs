using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankAPI.IContracts;
using BankAPI.Models;

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

        [HttpPost("AddBank/name={name}&password={password}")]
        public dynamic AddBankStaffRequest ( string name, string password )
        {
            if ( name == null || password == null )
            {
                return NotFound("Name or password not provided");
            }
            else
            {
                return _staffService.AddBankStaff(name, password);
            }
        }

        [HttpGet("GetUserAccount/id={id}&userType={userType}")]
        public dynamic GetUserAccountRequest ( string id )
        {
            if ( id == null ) return NotFound("Id not found");

            return _staffService.GetUserAccount(id);
        }

    }
}
