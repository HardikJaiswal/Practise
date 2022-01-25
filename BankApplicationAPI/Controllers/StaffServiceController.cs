using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApplication.Models;
using BankApplication.Contracts;
using BankApplication;

namespace BankApplicationAPI.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class StaffServiceController : ControllerBase
    {
        private IBankStaffService _staffService;

        public StaffServiceController(IBankStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpPut]
        public IActionResult PutRequest(PutRequestForStaffController putTypeForStaffController)
        {
            switch (putTypeForStaffController)
            {
                case PutRequestForStaffController.IsTransferReverted:
                    var transaction = new Transaction()
                    {
                        Amount = 0,
                    }
                    if (_staffService.IsTransferReverted())
                        return Ok("Transaction Reverted Successfully");
                    else
                        return BadRequest("Some Error occured");
                default: 
                    return NotFound("No such type of request ");
            }
        }

        [HttpPatch]
        public IActionResult PatchRequest(PatchRequestForStaffController deleteTypeForStaffController)
        {
            switch (deleteTypeForStaffController)
            {
                case PatchRequestForStaffController.UpdateAccountStatus:
                    _staffService.UpdateAccountStatus(1313513);
                    return Ok("Account Status Updated");
                default:
                    return NotFound("No such type of request");
            }
        }

        [HttpPost]
        public dynamic PostRequest(PostRequestForStaffController postTypeForStaffController)
        {
            switch (postTypeForStaffController)
            {
                case PostRequestForStaffController.CreateAccount:
                    return _staffService.CreateAccount("Hardik");
                case PostRequestForStaffController.AddCurrency:
                    _staffService.AddCurrency("Dollar",45);
                    return Ok("Currency Added");
                default:
                    return NotFound("No such type of request.");
            }
        }

        [HttpGet]
        public dynamic GetRequest(GetRequestForStaffController getTypeForStaffController)
        {
            switch (getTypeForStaffController)
            {
                case GetRequestForStaffController.IsCurrencyExist:
                    return _staffService.IsCurrencyExist("Dollar");
                case GetRequestForStaffController.GetTransactionHistory:
                    return _staffService.GetTransactionList(1313513);
                default:
                    return NotFound("No such type of request.");
            }
        }
    }
}
