using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mutants.Core;
using Mutants.Services;
using System.Threading.Tasks;
using Mutants.Models;

namespace Mutants.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class MutantsController : ControllerBase
    {
        private readonly IDnaSequenceService _dnaSequenceService;

        public MutantsController(IDnaSequenceService dnaSequenceService)
        {
            _dnaSequenceService = dnaSequenceService;
        }

        [HttpPost(Name = nameof(RegisterDna))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterDna([FromBody] DnaForm form)
        {
            var isMutant = await _dnaSequenceService.EvaluateDnaSequenceAsync(form);
            if (isMutant) 
                return Ok(new ApiStatusResult("This is Mutant DNA")
                {
                    Detail = "Welcome to the future!"
                });

            return Forbid();
        }
    }
}
