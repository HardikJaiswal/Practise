using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankAPI.IContracts;
using BankAPI.Models;
using BankAPI.Service;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        ICurrencyServie _service;

        public CurrencyController ( ICurrencyServie currencyServie )
        {
            _service = currencyServie;
        }

        [HttpGet("iscurrencyavailable/{name}")]
        public dynamic IsCurrencyAvailableRequest ( string name )
        {
            if ( name != null )
            {
                try
                {
                    return _service.IsCurrencyAvailable(name.Trim().ToLower()) ?
                    Ok("This currency is available") :
                    Ok("This currency is not available");
                }
                catch ( Exception ex )
                {
                    return Utilities.StatusResponse(ex.Message, false);
                }
                
            }
            return NotFound("Name of currency not provided.");
        }

        [HttpPost("addcurrency")]
        public dynamic AddCurrencyRequest ([FromBody]Currency currency)
        {
            if ( currency != null )
            {
                currency.Name = currency.Name.Trim().ToLower();
                try
                {
                    if (!_service.IsCurrencyAvailable(currency.Name) && currency.ValueInINR != 0)
                        return _service.CreateCurrency(currency);
                }
                catch(Exception ex)
                {
                    return Utilities.StatusResponse(ex.Message, false);
                }
            }
            return NotFound("Complete data was not provided Or Currency is already present.");
        }
    }
}
