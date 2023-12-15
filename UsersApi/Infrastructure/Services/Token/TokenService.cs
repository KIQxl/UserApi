using Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Token
{
    public static class TokenService
    {
        public static string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Name", user.Name),
                new Claim("role", user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("vn8945hv4m89vhcum0cf30ji90jvwp"));

            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(expires: DateTime.Now.AddHours(7), claims: claims, signingCredentials: signinCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
