using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Repositories
{
    //This only makes sense if this is something that would require future extensions, otherwise it would make more sense not introducing this complexity.

    /// <summary>
    /// Generic Repository Interface for the domain
    /// </summary>
    /// <typeparam name="T">Generic type for the model class</typeparam>
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Get(CancellationToken cancellationToken);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);
        Task<T> Get(int id, CancellationToken cancellationToken);
        Task<int> Create(T entity, CancellationToken cancellationToken);
        Task Update(T entity, CancellationToken cancellationToken);
        Task Update(Expression<Func<T, bool>> filter, T entity, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Delete(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);
        Task DeleteManyAsync(IEnumerable<T> collection, CancellationToken cancellationToken);
    }
}
