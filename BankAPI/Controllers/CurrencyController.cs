using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankAPI.IContracts;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        ICurrencyServie _service;

        public CurrencyController ( [FromServices]ICurrencyServie currencyServie )
        {
            _service = currencyServie;
        }

        [HttpGet("IsCurrencyAvailable/{name}")]
        public IActionResult IsCurrencyAvailableRequest ( string name )
        {
            if ( name != null )
            {
                return _service.IsCurrencyAvailable(name.Trim().ToLower()) ?
                    Ok("This currency is available") :
                    Ok("This currency is not available");
            }
            else
                return NotFound("Name of currency not provided.");
        }

        [HttpPost("AddCurrency/name={name}&rate={rate}&bankId={bankId}")]
        public dynamic AddCurrencyRequest ( string name, double rate, string bankId )
        {
            if ( name != null && rate != 0 && !_service.IsCurrencyAvailable(name.Trim().ToLower()) )
            {
                _service.CreateCurrency(name!.Trim().ToLower(), rate, bankId);
                return Ok($"Currency:{name} with Exchange Rate to INR: Rs.{rate}");
            }
            else
                return NotFound("Complete data was not provided Or Currency is already present.");
        }
    }
}
