using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Moq;
using Mutants.Core.Filters;
using System;
using System.Collections.Generic;
using Xunit;

namespace Mutants.Core.Tests
{
    public class JsonExceptionFilter_OnExceptionShould
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ReturnApiStatusResultBasedOnEnvironment(bool isDevelopment)
        {
            const string exceptionMessage = "This is an exception!";
            const string exceptionStackTrace = "Test stacktrace";
            var hostMock = new Mock<IHostEnvironment>();
            hostMock.SetupGet(x => x.EnvironmentName).Returns(isDevelopment ? Environments.Development : Environments.Staging);
            var actionContext = new ActionContext
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };
            var mockException = new Mock<Exception>();

            mockException.Setup(e => e.StackTrace)
            .Returns(exceptionStackTrace);
            mockException.Setup(e => e.Message)
            .Returns(exceptionMessage);
            mockException.Setup(e => e.Source)
            .Returns("Test source");

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = mockException.Object
            };

            var filter = new JsonExceptionFilter(hostMock.Object);
            filter.OnException(exceptionContext);
            var result = Assert.IsType<ObjectResult>(exceptionContext.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            var apiStatusResult = Assert.IsType<ApiStatusResult>(result.Value);
            if (!isDevelopment)
            {
                Assert.Equal("A server error occurred.", apiStatusResult.Message);
                Assert.Equal(exceptionMessage, apiStatusResult.Detail);
            }
            else
            {
                Assert.Equal(exceptionMessage, apiStatusResult.Message);
                Assert.Equal(exceptionStackTrace, apiStatusResult.Detail);
            }
        }
    }
}