using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Mutants.Services.Tests
{
    public class DnaSequenceService_GetDnaStatsAsyncShould : BaseDnaSequenceServiceTests
    {
        [Theory]
        [InlineData(0, 10, 1)]
        [InlineData(10, 0, 0)]
        [InlineData(10, 10, 1)]
        [InlineData(10, 5, 0.5)]
        [InlineData(5, 10, 2)]
        [InlineData(100, 40, 0.4)]        
        [InlineData(1000, 5, 0.005)]
        public async Task ReturnStatsWithRatio(int numberOfHumans, int numberOfMutants, decimal expectedRatio)
        {
            var dnaSequenceService = SetupDnaSequenceService(numberOfHumans, numberOfMutants);
            var stats = await dnaSequenceService.GetDnaStatsAsync();
            Assert.NotNull(stats);
            Assert.Equal(numberOfHumans, stats.NumberOfHumanSequences);
            Assert.Equal(numberOfMutants, stats.NumberOfMutantSequences);
            Assert.Equal(expectedRatio, stats.Ratio);
        }

        private DnaSequenceService SetupDnaSequenceService(int numberOfHumans, int numberOfMutants)
        {
            var dnaSequenceEvaluatorMock = new Mock<IDnaSequenceEvaluator>();

            var dnaSequenceService = SetupDnaSequenceService(dnaSequenceEvaluatorMock.Object);
            _dnaRepositoryMock.Setup(x => x.GetNumberOfHumanDnaSequences()).Returns(Task.FromResult((long)numberOfHumans));
            _dnaRepositoryMock.Setup(x => x.GetNumberOfMutantDnaSequences()).Returns(Task.FromResult((long)numberOfMutants));
            return dnaSequenceService;
        }
    }
}
