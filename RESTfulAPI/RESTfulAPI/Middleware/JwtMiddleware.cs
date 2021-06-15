using Microsoft.AspNetCore.Http;
using RESTfulAPI.JWTHelper;
using RESTfulAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPI.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context,IJWTHelper iJWTHelper)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrEmpty(token))
            {
                var email = iJWTHelper.ValidateToken(token);
                if (!string.IsNullOrEmpty(email))
                {
                    context.Items["IsValidRequest"] = true;
                }
            }
            await _next(context);
        }
    }
}
