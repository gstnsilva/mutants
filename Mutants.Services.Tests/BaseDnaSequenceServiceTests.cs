using Moq;
using Mutants.DomainModels;
using Mutants.ResourceAccess;

namespace Mutants.Services.Tests
{
    public abstract class BaseDnaSequenceServiceTests
    {
        protected Mock<IDnaRepository> _dnaRepositoryMock;
        
        protected DnaSequenceService SetupDnaSequenceService(IDnaSequenceEvaluator sequenceEvaluator)
        {
            _dnaRepositoryMock = new Mock<IDnaRepository>();
            _dnaRepositoryMock.Setup(x => x.AddOneAsync(It.IsAny<DnaSequence>()));

            return new DnaSequenceService(_dnaRepositoryMock.Object, sequenceEvaluator);
        }
    }
}