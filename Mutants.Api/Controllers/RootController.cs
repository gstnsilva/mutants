using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mutants.Core;
using Mutants.Core.Caching;
using Mutants.Core.Forms;
using Mutants.Core.Infrastructure;
using Mutants.Models;

namespace Mutants.Api.Controllers
{
    [Route("/")]
    [ApiController]
    public class RootController : ControllerBase
    {
        private HttpRequestHelper _httpRequestHelper;
        public RootController(HttpRequestHelper httpRequestHelper)
        {
            _httpRequestHelper = httpRequestHelper;
        }

        [HttpGet(Name = nameof(GetRoot))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ResponseCache(CacheProfileName = "Static")]
        [Etag]
        public IActionResult GetRoot()
        {
            var response = new RootResponse
            {
                Self = Link.To(nameof(GetRoot)),
                Info = Link.To(nameof(InfoController.GetInfo)),
                GetStats = Link.To(nameof(StatsController.GetStats)),
                RegisterDna = FormMetadata.FromModel(
                    new DnaForm(),
                    Link.ToForm(nameof(MutantController.RegisterDna), relations: Form.CreateRelation))
            };

            if (!_httpRequestHelper.GetEtagHandler(Request).NoneMatch(response))
            {
                return StatusCode(StatusCodes.Status304NotModified, response);
            }

            return Ok(response);
        }
    }
}