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
        public async Task<bool> IsUserExistsAsync(string email)
        {
            bool isUser = false;
            var users = await iUserRepository.FindByAsync(x => x.Email.Equals(email));
            if (users != null && users.Count() > 0)
            {
                isUser = true;
            }

            return isUser;
        }
    }
} 