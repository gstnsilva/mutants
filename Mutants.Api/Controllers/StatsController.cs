using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mutants.Core;
using Mutants.Models;
using Mutants.Services;
using System.Threading.Tasks;

namespace Mutants.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IDnaSequenceService _dnaSequenceService;

        public StatsController(IDnaSequenceService dnaSequenceService)
        {
            _dnaSequenceService = dnaSequenceService;
        }

        [HttpGet(Name = nameof(GetStats))]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetStats()
        {
            var dnaStats = await _dnaSequenceService.GetDnaStatsAsync();
            return Ok(new StatsResponse 
            {
                Self= Link.To(nameof(GetStats)),
                Stats = dnaStats
            });
        }
    }
}
