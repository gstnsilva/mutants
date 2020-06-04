﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace Mutants.Core.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _env;

        public JsonExceptionFilter(IHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var error = new ApiStatusResult();
            if (_env.IsDevelopment())
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A server error occurred.";
                error.Detail = context.Exception.Message;
            }

            context.Result = new ObjectResult(error)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
