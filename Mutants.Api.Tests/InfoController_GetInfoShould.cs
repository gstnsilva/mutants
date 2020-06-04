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
    public class InfoController_GetInfoShould : BaseControllerTest
    {
        #region Fields

        private const string InfoTitle = "Test Title";
        private const string InfoTagline = "Test Tagline";

        #endregion Fields

        #region Test Methods

        [Fact]
        public void ReturnUpdatedInfo_WhenNoneEtagMatched()
        {
            var infoController = SetupInfoController(false);

            var result = infoController.GetInfo();
            
            var info = AssertResult<OkObjectResult, Info>(result, StatusCodes.Status200OK);
            Assert.Equal(InfoTitle, info.Title);
            Assert.Equal(InfoTagline, info.Tagline);
        }

        [Fact]
        public void ReturnNotModified_WhenEtagMatched()
        {
            var infoController = SetupInfoController(true);

            var result = infoController.GetInfo();

            var info = AssertResult<ObjectResult, Info>(result, StatusCodes.Status304NotModified);

            Assert.Equal(InfoTitle, info.Title);
            Assert.Equal(InfoTagline, info.Tagline);
        }

        #endregion Test Methods

        #region Test Helpers

        private InfoController SetupInfoController(bool etagMatched)
        {            
            var info = new Info();
            info.Title = InfoTitle;
            info.Tagline = InfoTagline;

            var optionsMock = new Mock<IOptions<Info>>();
            optionsMock.SetupGet(x => x.Value).Returns(info);

            var requestHelperMock = SetupHttpRequestHelper(etagMatched);

            var infoController = new InfoController(optionsMock.Object, requestHelperMock.Object);
            return infoController;
        }

        #endregion Test Helpers
    }
}