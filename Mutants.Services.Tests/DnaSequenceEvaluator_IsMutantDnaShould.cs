using Microsoft.Extensions.Options;
using Moq;
using Mutants.Models;
using Xunit;

namespace Mutants.Services.Tests
{    
    public class DnaSequenceEvaluator_IsMutantDnaShould
    {
        private static readonly DnaEvaluationSettings _settings = new DnaEvaluationSettings
        {
            NumberOfDupeBasesRequired = 4,
            MinimumNumberOfMutantSequencesRequired = 2
        };

        [Theory]
        [MemberData(nameof(DnaSequenceServiceTestData.GetMutantSequences), MemberType = typeof(DnaSequenceServiceTestData))]
        public void ReturnTrueWhenSequenceContainsTwoOrMoreMutantBases(DnaForm dnaForm)
        {
            var evaluator = SetupDnaSequenceEvaluator();
            var isMutant = evaluator.IsMutantDna(dnaForm.Dna);
            Assert.True(isMutant); 
        }

        [Theory]
        [MemberData(nameof(DnaSequenceServiceTestData.GetHumanSequences), MemberType = typeof(DnaSequenceServiceTestData))]
        public void ReturnFalseWhenSequenceContainsLessThanTwoMutantBases(DnaForm dnaForm)
        {
            var evaluator = SetupDnaSequenceEvaluator();
            var isMutant = evaluator.IsMutantDna(dnaForm.Dna);
            Assert.False(isMutant); 
        }

        private DnaSequenceEvaluator SetupDnaSequenceEvaluator()
        {
            var optionsMock = new Mock<IOptions<DnaEvaluationSettings>>();
            optionsMock.SetupGet(x => x.Value).Returns(_settings);
            return new DnaSequenceEvaluator(optionsMock.Object);
        }
    }
}
