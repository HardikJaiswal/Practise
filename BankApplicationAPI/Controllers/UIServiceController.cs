using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApplication;
using BankApplication.Models;
using BankApplication.Contracts;

namespace BankApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UIServiceController : ControllerBase
    {
        IOperationalService _service;

        public UIServiceController(IOperationalService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult PostRequest(PostRequestForUIServiceController postTypeForUIServiceController)
        {
            switch (postTypeForUIServiceController)
            {
                case PostRequestForUIServiceController.AddBank:
                    _service.AddBank("SBI");
                    return Ok("Bank added successfully.");
                case PostRequestForUIServiceController.AddBankStaff:
                    _service.AddBankStaff("Hardik", "password");
                    return Ok("Bank Staff added successfully.");
                default:
                    return NotFound("No such type of request");
            }
        }

        [HttpGet]
        public dynamic GetRequest(GetRequestForUIServiceController getTypeForUIServiceController)
        {
            switch (getTypeForUIServiceController)
            {
                case GetRequestForUIServiceController.GetBankList:
                    return _service.GetBankList();
                case GetRequestForUIServiceController.GetUserAccount:
                    return _service.GetUserAccount("1313513", UserType.AccountHolder);
                default:
                    return NotFound("No such type of request");
            }
        }
    }
}
