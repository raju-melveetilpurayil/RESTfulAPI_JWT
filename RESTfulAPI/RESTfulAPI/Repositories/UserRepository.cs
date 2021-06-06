using RESTfulAPI.DataContext;
using RESTfulAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPI.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly RESTfulDataContext rESTfulDataContext;

        public UserRepository(RESTfulDataContext rESTfulDataContext) : base(rESTfulDataContext) => this.rESTfulDataContext = rESTfulDataContext;
    }
}
