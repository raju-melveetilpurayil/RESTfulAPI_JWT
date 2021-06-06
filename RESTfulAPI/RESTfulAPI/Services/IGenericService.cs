using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RESTfulAPI.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
    }
}
