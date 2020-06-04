using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Mutants.Api.Controllers;
using Mutants.Core.Caching;
using Mutants.Core.Infrastructure;
using Mutants.Models;
using Xunit;
using System.Threading.Tasks;

namespace Mutants.Api.Tests
{
    public class RootController_GetRootShould : BaseControllerTest
    {
        #region Test Methods

        [Fact]
        public void ReturnUpdatedRoot_WhenNoneEtagMatched()
        {
            var rootController = SetupRootController(false);

            var result = rootController.GetRoot();
            
            var rootResponse = AssertResult<OkObjectResult, RootResponse>(result, StatusCodes.Status200OK);
            AssertRootResponse(rootResponse);
        }

        [Fact]
        public void ReturnNotModified_WhenEtagMatched()
        {
            var rootController = SetupRootController(true);

            var result = rootController.GetRoot();

            var rootResponse = AssertResult<ObjectResult, RootResponse>(result, StatusCodes.Status304NotModified);
            AssertRootResponse(rootResponse);
        }

        #endregion Test Methods

        #region Test Helpers

        private RootController SetupRootController(bool etagMatched)
        {
            var requestHelperMock = SetupHttpRequestHelper(etagMatched);
            return new RootController(requestHelperMock.Object);
        }

        private void AssertRootResponse(RootResponse rootResponse)
        {
            Assert.NotNull(rootResponse.Self);

            Assert.NotNull(rootResponse.Info);
            Assert.NotNull(rootResponse.Info.RouteName);

            Assert.NotNull(rootResponse.RegisterDna);
            Assert.NotNull(rootResponse.RegisterDna.Self);
            Assert.NotNull(rootResponse.RegisterDna.Self.RouteName);
            Assert.NotNull(rootResponse.RegisterDna.Self.Relations);
            Assert.NotNull(rootResponse.RegisterDna.Self.Method);
            Assert.NotNull(rootResponse.RegisterDna.Value);

            Assert.NotNull(rootResponse.GetStats);
            Assert.NotNull(rootResponse.GetStats.RouteName);
        }

        #endregion Test Helpers
    }
}