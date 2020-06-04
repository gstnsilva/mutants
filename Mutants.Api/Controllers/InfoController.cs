using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mutants.Core;
using Mutants.Core.Caching;
using Mutants.Core.Infrastructure;
using Mutants.Models;

namespace Mutants.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly Info _info;
        private HttpRequestHelper _httpRequestHelper;

        public InfoController(IOptions<Info> hotelInfoWrapper, HttpRequestHelper httpRequestHelper)
        {
            _info = hotelInfoWrapper.Value;
            _info.Self = Link.To(nameof(GetInfo));
            _httpRequestHelper = httpRequestHelper;
        }

        [HttpGet(Name = nameof(GetInfo))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ResponseCache(CacheProfileName = "Static")]
        [Etag]
        public IActionResult GetInfo()
        {
            if (!_httpRequestHelper.GetEtagHandler(Request).NoneMatch(_info))
            {
                return StatusCode(StatusCodes.Status304NotModified, _info);
            }

            return Ok(_info);
        }
    }
}
