using Entities.DataModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Auth
{
    /// <summary>
    /// Creating a Jwt token.
    /// </summary>
    public static class TokenHandler
    {
        private const double JWT_Expiry = 1;

        public static string GenerateToken(string key, string issuer, string audience, User user)
        {
            var claims = new[]
            {
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("Id",user.Id.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(issuer, audience, claims,
                expires: DateTime.Now.AddDays(JWT_Expiry), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
