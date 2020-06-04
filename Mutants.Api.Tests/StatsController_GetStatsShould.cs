using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Mutants.Api.Controllers;
using Mutants.Core.Caching;
using Mutants.Core.Infrastructure;
using Mutants.Models;
using Mutants.Services;
using Xunit;
using System.Threading.Tasks;

namespace Mutants.Api.Tests
{
    public class StatsController_GetStatsShould : BaseControllerTest
    {        
        private readonly DnaStats _stats = new DnaStats 
        {
            NumberOfMutantSequences = 500,
            NumberOfHumanSequences = 100
        };

        #region Test Methods

        [Fact]
        public async Task ReturnStatusOkWithUpdatedStats()
        {
            var statsController = SetupStatsController();

            var result = await statsController.GetStats();
            
            var statsResponse = AssertResult<OkObjectResult, StatsResponse>(result, StatusCodes.Status200OK);
            Assert.NotNull(statsResponse);
            Assert.NotNull(statsResponse.Self);
            Assert.Equal(_stats.NumberOfHumanSequences, statsResponse.Stats.NumberOfHumanSequences);
            Assert.Equal(_stats.NumberOfMutantSequences, statsResponse.Stats.NumberOfMutantSequences);
        }

        #endregion Test Methods

        #region Test Helpers

        private StatsController SetupStatsController()
        {            
            var serviceMock = new Mock<IDnaSequenceService>();
            serviceMock.Setup(x => x.GetDnaStatsAsync()).Returns(Task.FromResult(_stats));

            return new StatsController(serviceMock.Object);
        }

        #endregion Test Helpers
    }
}