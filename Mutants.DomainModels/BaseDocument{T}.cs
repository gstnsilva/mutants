using MongoDbGenericRepository.Models;
using System;

namespace Mutants.DomainModels
{
    public abstract class BaseDocument<T> : IDocument<T> where T : IEquatable<T>
    {
        public T Id { get; set; }
        public int Version { get; set; }
    }
}
