using System.Collections.Generic;

namespace Mutants.Core.Tests
{
    public static class DnaSequenceTestData
    {
        public static IEnumerable<object[]> GetSequencesMatchingPattern() 
        {
            yield return new object[] { new[] {"A"} };
            yield return new object[] { new[] {"AA", "AT"} };
            yield return new object[] { new[] {"AAG", "ATC", "GTA"} };
            yield return new object[] { new[] {"AAGTCA", "AAGTCA", "AAGTCA", "AAGTCA", "AAGTCA", "AAGTCA"} };
        }

        public static IEnumerable<object[]> GetSequencesNotMatchingPattern() 
        {
            yield return new object[] { new[] {"B"} };
            yield return new object[] { new[] {"AB", "AA"} };
            yield return new object[] { new[] {"AAC", "ATM", "ATT"} };
            yield return new object[] { new[] {"ATC", "A%C", "G$A"} };
            yield return new object[] { new[] {"AAGTCA", "AAGTC", "AAGTCA", "AAGTCA", "AAGTCA", "AAGTC1"} };
        }

        public static IEnumerable<object[]> GetSequencesMatchingLength() 
        {
            yield return new object[] { new[] {"A"} };
            yield return new object[] { new[] {"AA", "AT"} };
            yield return new object[] { new[] {"AAG", "ATC", "GTA"} };
            yield return new object[] { new[] {"AAGTCA", "AAGTCA", "AAGTCA", "AAGTCA", "AAGTCA", "AAGTCA"} };
        }

        public static IEnumerable<object[]> GetSequencesNotMatchingLength() 
        {
            yield return new object[] { new[] {"AA"} };
            yield return new object[] { new[] {"AAC", "AT"} };
            yield return new object[] { new[] {"A", "ATC", "GTA"} };
            yield return new object[] { new[] {"AAGTCA", "AAGTC", "AAGTCA", "AAGTCA", "AAGTCA", "AAGTCA"} };
        }
    }
}