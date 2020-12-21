using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetClock.Application.Common.Configurations;
using NetClock.Application.Common.Interfaces.Identity;
using NetClock.Domain.Entities.Identity;

namespace NetClock.Application.Common.Services.Identity
{
    public class JwtSecurityTokenService : IJwtSecurityTokenService
    {
        private readonly JwtConfig _jwtConfig;

        public JwtSecurityTokenService(IOptions<JwtConfig> appSettings)
        {
            _jwtConfig = appSettings.Value;
        }

        public string CreateToken(ApplicationUser user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtConfig.ValidAudience,
                Issuer = _jwtConfig.ValidIssuer,
                Subject = AddClaimsRoles(user, roles),
                Expires = DateTime.UtcNow.AddHours(_jwtConfig.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static ClaimsIdentity AddClaimsRoles(ApplicationUser user, IEnumerable<string> roles)
        {
            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            foreach (var role in roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return claimsIdentity;
        }
    }
}
