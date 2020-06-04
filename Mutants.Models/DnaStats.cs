using Mutants.Core;
using Newtonsoft.Json;

namespace Mutants.Models
{
    public class DnaStats : Resource
    {
        [JsonProperty("count_mutant_dna")]
        public long NumberOfMutantSequences { get; set; }

        [JsonProperty("count_human_dna")]
        public long NumberOfHumanSequences { get; set; }

        public decimal Ratio => 
            NumberOfHumanSequences == 0 
                ? 1m 
                : (decimal)NumberOfMutantSequences/NumberOfHumanSequences;
    }
}