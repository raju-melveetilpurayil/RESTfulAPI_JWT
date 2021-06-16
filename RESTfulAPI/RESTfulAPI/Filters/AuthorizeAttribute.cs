using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isValid = false;
            Boolean.TryParse(context.HttpContext.Items["IsValidRequest"].ToString(), out isValid);
            if (!isValid)
            {
                context.Result = new UnauthorizedResult();
            }
            return;
        }
    }
}
