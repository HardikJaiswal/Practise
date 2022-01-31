using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankAPI.IContracts;
using BankAPI.Models;
using BankAPI.Service;

namespace BankAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BankServiceController : ControllerBase
    {
        private readonly IBankService _service;

        public BankServiceController ( IBankService service )
        {
            _service = service;
        }

        [HttpGet("gettransactions/{accountNumber}")]
        public dynamic GetTransactionsRequest ( int accountNumber )
        {
            if (accountNumber != 0)
            {
                try 
                {
                    return _service.GetTransactionHistory(accountNumber);
                }
                catch (Exception ex)
                {
                    return Utilities.StatusResponse(ex.Message,false);
                }
            }
            else
            {
                return NotFound("Account Number was not provided");
            }
        }

        [HttpPatch("tranfermoney/{transferMode}")]
        public dynamic MoneyTransferRequest ( [FromBody] Transaction details,TransferMode transferMode)
        {
            if (details.SrcAcc!= 0 && details.DestAcc != 0 && details.SrcBankId != String.Empty
                && details.DestBankId != String.Empty && details.Amount != 0)
            {
                try
                {
                    return _service.TransferMoney(details.SrcAcc, details.DestAcc, details.Amount,
                        details.SrcBankId, details.DestBankId, transferMode);
                }
                catch (Exception ex)
                {
                    return Utilities.StatusResponse(ex.Message,false);
                }
            }
            else
            {
                return BadRequest("Parameters were not provided correctly.");
            }
        }

        [HttpPatch("withdrawl/{accountNumber}&{amount}")]
        public dynamic MoneyWithdrawlRequest ( int accountNumber, int amount )
        {
            if (accountNumber != 0)
            {
                try
                {
                    return _service.WithdrawMoney(accountNumber, amount);
                }
                catch(Exception ex)
                {
                    return Utilities.StatusResponse(ex.Message,false);
                }
            }
            else
                return NotFound("Account Number not provided");
        }

        [HttpPatch("deposit/accountNumber={accountNumber}&amount={amount}")]
        public dynamic MoneyDepositRequest ( int accountNumber, int amount )
        {
            if ( accountNumber != 0 )
            {
                try
                {
                    return _service.DepositMoney(accountNumber, amount);
                }
                catch(Exception ex)
                {
                    return Utilities.StatusResponse(ex.Message,false);
                }
            }
            else
                return NotFound("AccountNumber not provided");
        }

        [HttpPost("addbank/{name}")]
        public dynamic AddBankRequest ( string name )
        {
            if ( name == null )
            {
                return NotFound("Bank Name not provided");
            }
            else
            {
                try
                {
                    return _service.AddBank(name);
                }
                catch (Exception ex)
                {
                    return Utilities.StatusResponse(ex.Message,false);
                }
            }
        }

        [HttpPatch("revertTransfer")]
        public dynamic RevertTransferRequest ( [FromBody] Transaction transaction )
        {
            if( transaction != null)
            {
                try
                {
                    return _service.RevertTransfer(transaction);
                }
                catch( Exception ex)
                {
                    return Utilities.StatusResponse(ex.Message,false);
                }
            }
            else
            {
                return BadRequest("The transaction was not found.");
            }
        }

        [HttpPatch("updatestatus/{accountNumber}")]
        public dynamic UpdateStatusRequest ( int accountNumber )
        {
            if ( accountNumber != 0 )
            {
                try
                {
                    return _service.UpdateAccountStatus(accountNumber);
                }
                catch ( Exception ex )
                {
                    return Utilities.StatusResponse(ex.Message, false);
                }
            }
            else
                return NotFound("Account Number not provided");
        }

        [HttpGet("getbanklist")]
        public dynamic GetBankNamesRequest()
        {
            try
            {
                return _service.GetBankNames();
            }
            catch ( Exception ex )
            {
                return Utilities.StatusResponse(ex.Message, false);
            }
        }

    }
}
