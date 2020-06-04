using Mutants.Core;
using System;

namespace Mutants.Models
{
    public class DnaStats : Resource
    {
        public long NumberOfMutantSequences { get; set; }
        public long NumberOfHumanSequences { get; set; }

        public decimal Ratio => 
            NumberOfHumanSequences == 0 
                ? 1m 
                : (decimal)NumberOfMutantSequences/NumberOfHumanSequences;
    }
}