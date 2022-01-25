using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIDemo.IContracts;
using APIDemo.Models;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UIServiceController : ControllerBase
    {
        IUIService _service;

        public UIServiceController([FromServices]IUIService service)
        {
            _service = service;
        }

        [HttpPost("AddBank/{name}")]
        public IActionResult AddBankRequest(string name)
        {
            if (name == null)
            {
                return NotFound("Bank Name not provided");
            }
            else
            {
                _service.AddBank(name);
                return Ok("Bank added successfully.");
            }
        }

        [HttpPost("AddBank/name={name}&password={password}")]
        public dynamic AddBankStaffRequest(string name,string password)
        {
            if (name == null || password == null)
            {
                return NotFound("Name or password not provided");
            }
            else
            {
                return _service.AddBankStaff(name, password);
            }
        }

        [HttpGet("GetBankList")]
        public List<string> GetBankNamesRequest()
        {
            return _service.GetBankNames();
        }

        [HttpGet("GetUserAccount/id={id}&userType={userType}")]
        public dynamic GetUserAccountRequest(string id,UserType userType)
        {
            if (id == null || userType == null)
                return NotFound("Id or type not found");
            return _service.GetUserAccount(id, userType);
        }
    }
}
