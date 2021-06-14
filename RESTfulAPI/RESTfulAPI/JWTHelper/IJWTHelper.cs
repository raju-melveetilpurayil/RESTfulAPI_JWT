using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPI.JWTHelper
{
    public interface IJWTHelper
    {
        string GetToken(string email);
    }
}
