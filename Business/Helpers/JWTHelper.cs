using Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Application.DTOs;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Business.Helpers
{
    public static class JWTHelper
    {
        public static JWTResultDTO GenerateJwtToken(AppUser user, string secretKey, ICollection<string> roles)
        {
            DateTime expire = DateTime.UtcNow.AddHours(24);
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = expire,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            if(!roles.IsNullOrEmpty())
            {
                foreach(var role in roles)
                    tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return new JWTResultDTO() { Expire = expire, Token = jwtToken };
        }
    }
}
