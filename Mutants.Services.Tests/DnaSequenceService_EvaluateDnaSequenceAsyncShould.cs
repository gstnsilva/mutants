using Moq;
using Mutants.DomainModels;
using Mutants.Models;
using Mutants.ResourceAccess;
using Mutants.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Mutants.Services.Tests
{    
    public class DnaSequenceService_EvaluateDnaSequenceAsyncShould : BaseDnaSequenceServiceTests
    {
        [Theory]
        [MemberData(nameof(DnaSequenceServiceTestData.GetMutantSequences), MemberType = typeof(DnaSequenceServiceTestData))]
        public async Task ReturnTrueWhenSequenceContainsTwoOrMoreMutantBases(DnaForm dnaForm)
        {
            var dnaSequenceService = SetupDnaSequenceService(true);
            var isMutant = await dnaSequenceService.EvaluateDnaSequenceAsync(dnaForm);
            Assert.True(isMutant); 
            _dnaRepositoryMock.Verify(x => x.AddOneAsync(It.IsAny<DnaSequence>()), Times.Once());
        }

        [Theory]
        [MemberData(nameof(DnaSequenceServiceTestData.GetHumanSequences), MemberType = typeof(DnaSequenceServiceTestData))]
        public async Task ReturnFalseWhenSequenceContainsLessThanTwoMutantBases(DnaForm dnaForm)
        {
            var dnaSequenceService = SetupDnaSequenceService(false);
            var isMutant = await dnaSequenceService.EvaluateDnaSequenceAsync(dnaForm);
            Assert.False(isMutant); 
            _dnaRepositoryMock.Verify(x => x.AddOneAsync(It.IsAny<DnaSequence>()), Times.Once());
        }

        private DnaSequenceService SetupDnaSequenceService(bool isMutant)
        {
            var dnaSequenceEvaluatorMock = new Mock<IDnaSequenceEvaluator>();
            dnaSequenceEvaluatorMock.Setup(x => x.IsMutantDna(It.IsAny<string[]>())).Returns(isMutant);

            return SetupDnaSequenceService(dnaSequenceEvaluatorMock.Object);
        }
    }
}
