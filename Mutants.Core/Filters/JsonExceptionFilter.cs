using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mutants.Core.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _env;
        private ILogger<JsonExceptionFilter> _logger;

        public JsonExceptionFilter(IHostEnvironment env, ILogger<JsonExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            _logger.LogError(exception, "Global Catch");

            var error = new ApiStatusResult();
            if (_env.IsDevelopment())
            {
                error.Message = exception.Message;
                error.Detail = exception.StackTrace;
            }
            else
            {
                error.Message = "A server error occurred.";
                error.Detail = exception.Message;
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
