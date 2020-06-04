
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mutants.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {        
        [HttpGet(Name = nameof(CheckHealth))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CheckHealth()
        {
            return Ok();
        }
    }
}