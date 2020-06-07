using MongoDbGenericRepository;
using Mutants.DomainModels;
using System;
using System.Threading.Tasks;

namespace Mutants.ResourceAccess
{
    public class DnaRepository : BaseRepository<DnaSequence, Guid>, IDnaRepository
    {
        public DnaRepository(MutantsDbContext dbContext) : base(dbContext) { }

        public Task<long> GetNumberOfHumanDnaSequences()
        {
            return CountAsync(x => !x.IsMutant);
        }

        public Task<long> GetNumberOfMutantDnaSequences()
        {
            return CountAsync(x => x.IsMutant);
        }

        public Task<DnaSequence> GetDnaSequenceAsync(string[] sequence)
        {
            return base.GetOneAsync<DnaSequence>(doc => doc.Sequence == sequence);
        }
    }
}