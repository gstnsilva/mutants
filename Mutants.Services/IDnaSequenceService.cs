using System.Threading.Tasks;
using Mutants.Models;

namespace Mutants.Services
{
    public interface IDnaSequenceService
    {
        Task<bool> EvaluateDnaSequenceAsync(DnaForm dnaForm);

        Task<DnaStats> GetDnaStatsAsync();
    }
}