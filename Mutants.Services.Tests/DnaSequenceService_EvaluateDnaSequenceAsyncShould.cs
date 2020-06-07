using Moq;
using Mutants.DomainModels;
using Mutants.Models;
using Mutants.ResourceAccess;
using Mutants.Services;
using System;
using System.Linq.Expressions;
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
        [MemberData(nameof(DnaSequenceServiceTestData.GetMutantSequences), MemberType = typeof(DnaSequenceServiceTestData))]
        public async Task ReturnTrueWhenMutantSequenceExistsInDb(DnaForm dnaForm)
        {
            var dnaSequenceService = SetupDnaSequenceServiceForExistingSequence(true);
            var isMutant = await dnaSequenceService.EvaluateDnaSequenceAsync(dnaForm);
            Assert.True(isMutant);
        }

        [Theory]
        [MemberData(nameof(DnaSequenceServiceTestData.GetHumanSequences), MemberType = typeof(DnaSequenceServiceTestData))]
        public async Task ReturnFalseWhenHumanSequenceExistsInDb(DnaForm dnaForm)
        {
            var dnaSequenceService = SetupDnaSequenceServiceForExistingSequence(false);
            var isMutant = await dnaSequenceService.EvaluateDnaSequenceAsync(dnaForm);
            Assert.False(isMutant);            
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

        private DnaSequenceService SetupDnaSequenceServiceForExistingSequence(bool isMutant)
        {
            var dnaSequenceEvaluatorMock = new Mock<IDnaSequenceEvaluator>();
            var dnaSequence = new DnaSequence {IsMutant = isMutant};
            _dnaRepositoryMock = new Mock<IDnaRepository>();
            _dnaRepositoryMock.Setup(x => x.GetDnaSequenceAsync(It.IsAny<string[]>())).Returns(Task.FromResult(dnaSequence));
            return new DnaSequenceService(_dnaRepositoryMock.Object, dnaSequenceEvaluatorMock.Object);
        }
    }
}
