using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Mutants.Core;
using Mutants.Core.Caching;
using Mutants.Core.Infrastructure;
using Mutants.Models;
using Xunit;
using System;

namespace Mutants.Api.Tests
{
    public abstract class BaseControllerTest
    {
        protected virtual Mock<HttpRequestHelper> SetupHttpRequestHelper(bool etagMatched)
        {
            var etagHandlerFeatureMock = new Mock<IEtagHandlerFeature>();
            etagHandlerFeatureMock.Setup(x => x.NoneMatch(It.IsAny<IEtaggable>())).Returns(!etagMatched);
            
            var requestHelperMock = new Mock<HttpRequestHelper>();
            requestHelperMock.Setup(x => x.GetEtagHandler(It.IsAny<HttpRequest>())).Returns(etagHandlerFeatureMock.Object);
            return requestHelperMock;
        }
        
        protected virtual TResultValue AssertResult<TObjectResult, TResultValue>(object result, int expectedStatusCode) 
            where TObjectResult : ObjectResult
        {
            var objectResult = Assert.IsType<TObjectResult>(result);
            Assert.Equal(expectedStatusCode, objectResult.StatusCode);
            return Assert.IsType<TResultValue>(objectResult.Value);
        }
    }
}