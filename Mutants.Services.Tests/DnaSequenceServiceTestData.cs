using Mutants.Models;
using System.Collections.Generic;

namespace Mutants.Services.Tests
{
    public static class DnaSequenceServiceTestData
    {
        private static readonly string[] _size2HumanDna = new[] {"AT", "CA"};
        private static readonly string[] _size4HumanDna = new[] {"ATGT", "CAGT", "TTAT", "AGAT"};
        private static readonly string[] _size6HumanDna = new[] {"ATGCGA", "CAGTGC", "TTTTTT", "AGACGG", "GCGTCA", "TCACTG"};
        private static readonly string[] _size8HumanDna = new[] {"ATGCGAGA", "CAGTGCGT", "TTATTTGG", "AGACGGTA", "GCGTCAAT", "TCACTGCA", "GCGTCAAT", "TGACTGCA"};
        private static readonly string[] _size4HorizonalMutantDna = new[] {"AAAA", "CAGT", "TTTT", "AGAC"};
        private static readonly string[] _size6VerticalMutantDna = new[] {"ATGCGC", "AAGTGC", "ATATTC", "AGACGC", "GCGTCA", "TCACTG"};
        private static readonly string[] _size6VerticalAndDiagonalMutantDna = new[] {"ATGCGA", "AGTGCA", "ATATGT", "AGAATG", "CCCCTT", "TCACTG"};
        private static readonly string[] _size9CrossMutantDna = new[] {"ATGCTGAGA", "CAGTTGCGT", "TTAATTTGG", "AGACTGGTA", "GCTTTTTAT", "TCACATGCA", "GCGTTCAAT", "TGACTTGCA", "ATGCTGAGA"};
        private static readonly string[] _size8TwiceDiagonalMutantDna = new[] {"ATGCGAGA", "CAGTGCGT", "TTATTTGG", "AGAAGGTA", "GCGTAAAT", "TCACTACA", "GCGTCAAT", "TGACTGCA"};
        private static readonly string[] _size8CrossDiagonalMutantDna = new[] {"ATGCGAGA", "CAGTGCAT", "TTATTAGG", "AGAAAGTA", "GCGAATAT", "TCACTACA", "GAGTCAAT", "AGACTGCA"};

        public static IEnumerable<object[]> GetHumanSequences() 
        {
            yield return new object[] { new DnaForm { Dna = _size2HumanDna } };
            yield return new object[] { new DnaForm { Dna = _size4HumanDna } };
            yield return new object[] { new DnaForm { Dna = _size6HumanDna } };
            yield return new object[] { new DnaForm { Dna = _size8HumanDna } };
        }

        public static IEnumerable<object[]> GetMutantSequences() 
        {
            yield return new object[] { new DnaForm { Dna = _size4HorizonalMutantDna } };
            yield return new object[] { new DnaForm { Dna = _size6VerticalMutantDna } };
            yield return new object[] { new DnaForm { Dna = _size6VerticalAndDiagonalMutantDna } };
            yield return new object[] { new DnaForm { Dna = _size9CrossMutantDna } };
            yield return new object[] { new DnaForm { Dna = _size8TwiceDiagonalMutantDna } };
            yield return new object[] { new DnaForm { Dna = _size8CrossDiagonalMutantDna } };
        }
    }
}