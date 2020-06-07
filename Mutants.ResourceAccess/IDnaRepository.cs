using Mutants.DomainModels;
using System;
using System.Threading.Tasks;

namespace Mutants.ResourceAccess
{
    public interface IDnaRepository : IBaseRepository<DnaSequence, Guid>
    {
        Task<long> GetNumberOfHumanDnaSequences();
        Task<long> GetNumberOfMutantDnaSequences();
        Task<DnaSequence> GetDnaSequenceAsync(string[] sequence);
    }
}