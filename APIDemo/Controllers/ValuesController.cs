using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("In HTTP get methods");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return NotFound("Nothing was present to post.");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return BadRequest("Nothing was provided to update.");
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("Nothing to delete.");
        }
    }
}
