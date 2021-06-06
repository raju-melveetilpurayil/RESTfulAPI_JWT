using Microsoft.EntityFrameworkCore;
using RESTfulAPI.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RESTfulAPI.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
       where T : class
    {
        private readonly RESTfulDataContext rESTfulDataContext;

        public GenericRepository(RESTfulDataContext rESTfulDataContext)
        {
            this.rESTfulDataContext = rESTfulDataContext;
        }

        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> resultSet;
            try
            {
                resultSet = await rESTfulDataContext.Set<T>().Where(predicate).ToListAsync();
            }
            catch 
            {
                throw ;
            }
            return resultSet;
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await rESTfulDataContext.Set<T>().AddAsync(entity);
                await rESTfulDataContext.SaveChangesAsync();
            }
            catch
            {
                throw ;
            }
            return entity;
        }

    }
}
