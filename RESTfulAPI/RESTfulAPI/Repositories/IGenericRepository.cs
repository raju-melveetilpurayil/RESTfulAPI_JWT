using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RESTfulAPI.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
    }
}
