using Microsoft.AspNetCore.Http;
using Mutants.Core.Caching;

namespace Mutants.Core.Infrastructure
{
    public class HttpRequestHelper
    {
        public virtual IEtagHandlerFeature GetEtagHandler(HttpRequest request)
            => request.HttpContext.Features.Get<IEtagHandlerFeature>();
    }
}
