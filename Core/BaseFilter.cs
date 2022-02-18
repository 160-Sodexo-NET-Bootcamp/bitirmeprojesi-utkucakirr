using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Core
{
    /// <summary>
    /// Base model for filters. This model gets Id value from Jwt token.
    /// </summary>
    public class BaseFilter
    {
        private StringValues token;

        public int GetToken(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            request.Headers.TryGetValue("Authorization", out token);
            var str = token.ToString().Trim().Substring(7);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(str) as JwtSecurityToken;

            return Convert.ToInt32(jsonToken.Claims.First(x => x.Type == "Id").Value);
        }
    }
}
