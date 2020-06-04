using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Models;

namespace Mutants.ResourceAccess
{
    public interface IBaseRepository<TDocument, TKey> : IBaseMongoRepository<TKey> 
        where TDocument : IDocument<TKey>
        where TKey : IEquatable<TKey>
    {
        Task AddOneAsync(TDocument document);

        Task<long> DeleteOneAsync(TDocument document);
    }
}
