using Mutants.Core.Validation;
using Xunit;

namespace Mutants.Core.Tests
{
    public class StringArrayRegularExpression_IsValidShould
    {
        private readonly StringArrayRegularExpression _validator = new StringArrayRegularExpression("^[ACGTacgt]+$");

        [Theory]
        [MemberData(nameof(DnaSequenceTestData.GetSequencesMatchingPattern), MemberType = typeof(DnaSequenceTestData))]
        public void ReturnTrue_WhenPatternMatches(string[] dnaSequence)
        {
            var isValid = _validator.IsValid(dnaSequence);
            Assert.True(isValid);
        }
        
        [Theory]
        [MemberData(nameof(DnaSequenceTestData.GetSequencesNotMatchingPattern), MemberType = typeof(DnaSequenceTestData))]
        public void ReturnFalse_WhenPatternDoesntMatch(string[] dnaSequence)
        {
            var isValid = _validator.IsValid(dnaSequence);
            Assert.False(isValid);
        }
    }
}