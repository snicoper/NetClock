using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetClock.Application.Configurations;
using NetClock.Application.Interfaces.Identity;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Services.Identity
{
    public class JwtSecurityTokenService : IJwtSecurityTokenService
    {
        private readonly AppSettings _appSettings;

        public JwtSecurityTokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string CreateToken(ApplicationUser user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Jwt.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _appSettings.Jwt.ValidAudience,
                Issuer = _appSettings.Jwt.ValidIssuer,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, string.Join(",", roles.ToList()))
                }),
                Expires = DateTime.UtcNow.AddHours(_appSettings.Jwt.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
