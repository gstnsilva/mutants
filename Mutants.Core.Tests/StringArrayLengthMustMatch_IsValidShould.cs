using Mutants.Core.Validation;
using Xunit;

namespace Mutants.Core.Tests
{
    public class StringArrayLengthMustMatch_IsValidShould
    {
        [Theory]
        [MemberData(nameof(DnaSequenceTestData.GetSequencesMatchingLength), MemberType = typeof(DnaSequenceTestData))]
        public void ReturnTrue_WhenLengthMatches(string[] dnaSequence)
        {
            var isValid = new StringArrayLengthMustMatch().IsValid(dnaSequence);
            Assert.True(isValid);
        }
        
        [Theory]
        [MemberData(nameof(DnaSequenceTestData.GetSequencesNotMatchingLength), MemberType = typeof(DnaSequenceTestData))]
        public void ReturnFalse_WhenLengthDoesntMatch(string[] dnaSequence)
        {
            var isValid = new StringArrayLengthMustMatch().IsValid(dnaSequence);
            Assert.False(isValid);
        }
    }
}