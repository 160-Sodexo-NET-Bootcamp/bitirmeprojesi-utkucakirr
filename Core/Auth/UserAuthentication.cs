using Entities.DataModel;
using Microsoft.Extensions.Configuration;

namespace Core.Auth
{
    /// <summary>
    /// Creating a Jwt token.
    /// </summary>
    public static class UserAuthentication
    {
        public static Response Auth(User user, IConfiguration configuration)
        {
            var key = configuration["Jwt:Key"];
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            string token = TokenHandler.GenerateToken(key, issuer, audience, user);

            if (string.IsNullOrWhiteSpace(token))
            {
                return new Response {IsSuccess = false};
            }

            return new Response {IsSuccess = true, AccessToken = token};
        }
    }
}
