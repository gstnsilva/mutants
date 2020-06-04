using MongoDbGenericRepository;

namespace Mutants.ResourceAccess
{
    public class MutantsDbContext : MongoDbContext
    {
        public MutantsDbContext(MutantsDbSettings settings) 
            : base(settings.ConnectionString, settings.DatabaseName) { }
    }
}