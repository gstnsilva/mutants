using MongoDbGenericRepository.Attributes;
using System;

namespace Mutants.DomainModels
{
    [CollectionName("dnaSequences")]
    public class DnaSequence : BaseDocument<Guid>
    {
        public string[] Sequence { get; set; }
        public bool IsMutant { get; set; }
    }
}