using RESTfulAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RESTfulAPI.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> iGenericRepository;
        public GenericService(IGenericRepository<T> iGenericRepository)
        {
            this.iGenericRepository = iGenericRepository;
        }
        public async Task<T> AddAsync(T entity)
        {
            return await iGenericRepository.AddAsync(entity);
        }
        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await iGenericRepository.FindByAsync(predicate);
        }
    }
}
