using RESTfulAPI.Models;
using RESTfulAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPI.Services
{
    public class UserService:GenericService<User>,IUserService
    {
        private readonly IUserRepository iUserRepository;
        public UserService(IUserRepository iUserRepository):base(iUserRepository)
        {
            this.iUserRepository = iUserRepository;
        }
    }
} 