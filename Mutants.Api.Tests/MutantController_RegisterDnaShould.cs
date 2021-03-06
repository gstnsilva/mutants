using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Mutants.Api.Controllers;
using Mutants.Core;
using Mutants.Core.Caching;
using Mutants.Core.Infrastructure;
using Mutants.Models;
using Mutants.Services;
using Xunit;
using System.Threading.Tasks;

namespace Mutants.Api.Tests
{
    public class MutantController_RegisterDnaShould : BaseControllerTest
    {
        #region Test Methods

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ReturnStatusCodeToIdentifyMutants(bool isMutantDna)
        {
            var mutantsController = SetupMutantsController(isMutantDna);

            var result = await mutantsController.RegisterDna(new DnaForm());
            
            if (isMutantDna)
            {
                var statusResult = AssertResult<OkObjectResult, ApiStatusResult>(result, StatusCodes.Status200OK);
                Assert.Equal("This is Mutant DNA", statusResult.Message);
                Assert.Equal("Welcome to the future!", statusResult.Detail);
            }
            else
            {
                var statusResult = AssertResult<ObjectResult, ApiStatusResult>(result, StatusCodes.Status403Forbidden);
                Assert.Equal("This is human DNA", statusResult.Message);
                Assert.Equal("Peace was never an option.", statusResult.Detail);
            }
        }

        #endregion Test Methods

        #region Test Helpers

        private MutantController SetupMutantsController(bool isMutant)
        {
            var serviceMock = new Mock<IDnaSequenceService>();
            serviceMock.Setup(x => x.EvaluateDnaSequenceAsync(It.IsAny<DnaForm>())).Returns(Task.FromResult(isMutant));

            return new MutantController(serviceMock.Object);
        }

        #endregion Test Helpers
    }
}