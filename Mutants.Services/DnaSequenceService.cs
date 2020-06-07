using Mutants.DomainModels;
using Mutants.Models;
using Mutants.ResourceAccess;
using System.Linq;
using System.Threading.Tasks;

namespace Mutants.Services
{
    public class DnaSequenceService : IDnaSequenceService
    {
        private readonly IDnaRepository _dnaRepository;
        private readonly IDnaSequenceEvaluator _dnaSequenceEvaluator;

        public DnaSequenceService(IDnaRepository dnaRepository, IDnaSequenceEvaluator dnaSequenceEvaluator)
        {
            _dnaRepository = dnaRepository;
            _dnaSequenceEvaluator = dnaSequenceEvaluator;
        }

        public async Task<bool> EvaluateDnaSequenceAsync(DnaForm dnaForm)
        {
            var sequence = dnaForm.Dna.Select(x => x.ToUpper()).ToArray();
            var existingDna = await _dnaRepository.GetDnaSequenceAsync(sequence);
            if (null != existingDna)
                return existingDna.IsMutant;

            var isMutant = _dnaSequenceEvaluator.IsMutantDna(sequence);
            var dnaSequence = new DnaSequence 
            { 
                Sequence = sequence,
                IsMutant= isMutant
            };
            await _dnaRepository.AddOneAsync(dnaSequence);
            return isMutant;
        }

        public async Task<DnaStats> GetDnaStatsAsync()
        {
            var mutantsTask = _dnaRepository.GetNumberOfMutantDnaSequences();
            var humansTask = _dnaRepository.GetNumberOfHumanDnaSequences();

            var numberOfMutants = await mutantsTask;
            var numberOfHumans = await humansTask;
            return new DnaStats {NumberOfHumanSequences = numberOfHumans, NumberOfMutantSequences = numberOfMutants};
        }
    }
}
