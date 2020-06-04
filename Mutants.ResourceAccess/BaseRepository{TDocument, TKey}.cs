using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDbGenericRepository;
using Mutants.DomainModels;

namespace Mutants.ResourceAccess
{
    public abstract class BaseRepository<TDocument, TKey> : BaseMongoRepository<TKey>, IBaseRepository<TDocument, TKey>
        where TDocument : BaseDocument<TKey>
        where TKey : IEquatable<TKey>
    {
        protected BaseRepository(MongoDbContext dbContext)
            : base(dbContext) { }

        public Task AddOneAsync(TDocument document)
        {
            return base.AddOneAsync(document);
        }

        public Task<long> DeleteOneAsync(TDocument document)
        {
            return base.DeleteOneAsync(document);
        }

        protected Task<long> CountAsync(Expression<Func<TDocument, bool>> filter, string partitionKey = null)
        {
            return base.CountAsync(filter, partitionKey);
        }
    }
}
