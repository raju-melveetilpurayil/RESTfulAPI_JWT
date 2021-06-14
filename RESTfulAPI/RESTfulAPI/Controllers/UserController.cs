using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RESTfulAPI.Helpers;
using RESTfulAPI.Models;
using RESTfulAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService,IConfiguration configuration, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.configuration = configuration;
            this.logger = logger;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public  JsonResult Login(string email,string password)
        {
            return new JsonResult("Not Implimented");
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<JsonResult> Register(string email,string password)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                var isUserExists = await userService.IsUserExistsAsync(email);
                if (!isUserExists)
                {
                    User user = new Models.User()
                    {
                        Email = email,
                        Password = new PasswordHash(configuration).GetHash(password),
                        DateCreated = DateTime.Now
                    };
                    user = await userService.AddAsync(user);
                    message = "Registerd successfully, Please login now";
                }
                else
                {
                    message = "User already exists, please use any other email address";
                }
            }
            return new JsonResult(message);
        }

    }
}
